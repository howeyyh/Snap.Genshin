using DGP.Snap.Framework.Extensions;
using System;
using System.IO;
using System.Threading;

namespace DGP.Snap.Framework.Net.Download
{
    internal class StreamCopyWorker : IStreamCopyWorker, IDisposable
    {
        private const int DefaultBufferSize = 1024 * 1024;
        private readonly ManualResetEvent streamCopyFinished = new ManualResetEvent(true);
        private readonly System.Timers.Timer progressUpdateTimer;
        private readonly TimeSpan safeWaitTimeout;
        private readonly int copyBufferSize;

        private Stream sourceStream;
        private Stream destinationStream;
        private long previousReportedBytesReceived;
        private bool disposed;
        private long totalBytes;
        private CompletedState completedState;
        private int workerState = (int)WorkerState.NotStarted;

        public StreamCopyWorker()
            : this(TimeSpan.FromMilliseconds(500), DefaultBufferSize, TimeSpan.FromSeconds(15))
        {
        }

        internal StreamCopyWorker(TimeSpan progressUpdateInterval, int copyBufferSize, TimeSpan safeWaitTimeout)
        {
            this.copyBufferSize = copyBufferSize;
            this.safeWaitTimeout = safeWaitTimeout;

            progressUpdateTimer = new System.Timers.Timer(progressUpdateInterval.TotalMilliseconds);
            progressUpdateTimer.Elapsed += OnProgressUpdateTimerElapsed;
        }

        public event EventHandler<StreamCopyCompleteEventArgs> Completed;

        public event EventHandler<StreamCopyProgressEventArgs> ProgressChanged;

        private enum WorkerState
        {
            NotStarted,
            Started,
            Canceled,
            Finished
        }

        public long Position { get; private set; }

        public void CopyAsync(Stream source, Stream destination, long sizeInBytes)
        {
            if (ChangeState(WorkerState.Started) == false)
            {
                return;
            }

            sourceStream = source;
            destinationStream = destination;
            totalBytes = sizeInBytes;

            ThreadPool.QueueUserWorkItem(stateInfo => RunCopyProcess());
        }

        public void Cancel()
        {
            if (ChangeState(WorkerState.Canceled) == false)
            {
                return;
            }

            if (!streamCopyFinished.WaitOne(safeWaitTimeout))
            {
                return;
            }

            ////We may reach this code when cancel is requested BEFORE the RunCopyProcess
            ////There are moments when worker is not started, but still can be cancelled.
            FinalizeStream(ref destinationStream);
            FinalizeStream(ref sourceStream);
        }

        private void OnCompleted(StreamCopyCompleteEventArgs args) => Completed.SafeInvoke(this, args);

        private void OnProgressChanged(StreamCopyProgressEventArgs args) => ProgressChanged.SafeInvoke(this, args);

        private void RunCopyProcess()
        {
            if (!InitializeCopyProcess())
            {
                return;
            }

            Exception error = null;
            try
            {
                Copy();
                EmitFinalProgress();
            }
            catch (Exception ex)
            {
                completedState = CompletedState.Failed;
                error = ex;
            }

            FinalizeCopyProcess(error);
        }

        private bool InitializeCopyProcess()
        {
            if (!streamCopyFinished.WaitOne(safeWaitTimeout))
            {
                return false;
            }
            streamCopyFinished.Reset();

            Position = 0;
            ChangeState(WorkerState.Started);

            progressUpdateTimer.Start();
            return true;
        }

        private void Copy()
        {
            var buffer = new byte[copyBufferSize];
            using (var binaryWriter = new BinaryWriter(destinationStream))
            {
                int readBytes;
                while ((readBytes = sourceStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    binaryWriter.Write(buffer, 0, readBytes);
                    Position = destinationStream.Position;

                    if (GetState() == WorkerState.Canceled || GetState() == WorkerState.Finished)
                    {
                        completedState = CompletedState.Canceled;
                        return;
                    }
                }

                completedState = CompletedState.Succeeded;
            }
        }

        private void EmitFinalProgress()
        {
            ////the last direct call to deliver most accurate and actual progress without timer
            ////without this we often have not 100% on the end of download
            if (completedState == CompletedState.Succeeded)
            {
                if (Position != totalBytes)
                {
                    throw new System.Exception(String.Format("Stream incomplete. Expected size: {0}, actual size {1}", totalBytes, Position));
                }

                OnProgressChanged(new StreamCopyProgressEventArgs { BytesReceived = Position });
            }
        }

        private void FinalizeCopyProcess(System.Exception error)
        {
            progressUpdateTimer.Stop();

            FinalizeStream(ref sourceStream);
            FinalizeStream(ref destinationStream);

            ChangeState(WorkerState.Finished);

            streamCopyFinished.Set();
            OnCompleted(new StreamCopyCompleteEventArgs { CompleteState = completedState, Exception = error });
        }

        private void FinalizeStream(ref Stream stream)
        {
            if (stream == null)
            {
                return;
            }

            try
            {
                stream.Dispose();
                stream = null;
            }
            catch (Exception)
            {
            }
        }

        private void OnProgressUpdateTimerElapsed(object sender, EventArgs eventArgs)
        {
            if (GetState() != WorkerState.Started)
            {
                return;
            }

            if (Position != previousReportedBytesReceived)
            {
                previousReportedBytesReceived = Position;
                OnProgressChanged(new StreamCopyProgressEventArgs { BytesReceived = Position });
            }
        }

        private bool ChangeState(WorkerState newState)
        {
            if (newState == WorkerState.Finished)
            {
                Interlocked.Exchange(ref workerState, (int)newState);
                return true;
            }
            return Interlocked.CompareExchange(ref workerState, (int)newState, (int)newState - 1) == (int)newState - 1;
        }

        private WorkerState GetState() => (WorkerState)workerState;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    progressUpdateTimer.Dispose();
                    streamCopyFinished.Close();
                    ChangeState(WorkerState.Finished);
                    FinalizeStream(ref sourceStream);
                    FinalizeStream(ref destinationStream);
                }
                disposed = true;
            }
        }
    }
}