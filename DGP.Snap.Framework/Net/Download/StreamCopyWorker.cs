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

            this.progressUpdateTimer = new System.Timers.Timer(progressUpdateInterval.TotalMilliseconds);
            this.progressUpdateTimer.Elapsed += this.OnProgressUpdateTimerElapsed;
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
            if (this.ChangeState(WorkerState.Started) == false)
            {
                return;
            }

            this.sourceStream = source;
            this.destinationStream = destination;
            this.totalBytes = sizeInBytes;

            ThreadPool.QueueUserWorkItem(stateInfo => this.RunCopyProcess());
        }

        public void Cancel()
        {
            if (this.ChangeState(WorkerState.Canceled) == false)
            {
                return;
            }

            if (!this.streamCopyFinished.WaitOne(this.safeWaitTimeout))
            {
                return;
            }

            ////We may reach this code when cancel is requested BEFORE the RunCopyProcess
            ////There are moments when worker is not started, but still can be cancelled.
            this.FinalizeStream(ref this.destinationStream);
            this.FinalizeStream(ref this.sourceStream);
        }

        private void OnCompleted(StreamCopyCompleteEventArgs args) => Completed.SafeInvoke(this, args);

        private void OnProgressChanged(StreamCopyProgressEventArgs args) => ProgressChanged.SafeInvoke(this, args);

        private void RunCopyProcess()
        {
            if (!this.InitializeCopyProcess())
            {
                return;
            }

            Exception error = null;
            try
            {
                this.Copy();
                this.EmitFinalProgress();
            }
            catch (Exception ex)
            {
                this.completedState = CompletedState.Failed;
                error = ex;
            }

            this.FinalizeCopyProcess(error);
        }

        private bool InitializeCopyProcess()
        {
            if (!this.streamCopyFinished.WaitOne(this.safeWaitTimeout))
            {
                return false;
            }
            this.streamCopyFinished.Reset();

            this.Position = 0;
            this.ChangeState(WorkerState.Started);

            this.progressUpdateTimer.Start();
            return true;
        }

        private void Copy()
        {
            var buffer = new byte[this.copyBufferSize];
            using (var binaryWriter = new BinaryWriter(this.destinationStream))
            {
                int readBytes;
                while ((readBytes = this.sourceStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    binaryWriter.Write(buffer, 0, readBytes);
                    this.Position = this.destinationStream.Position;

                    if (this.GetState() == WorkerState.Canceled || this.GetState() == WorkerState.Finished)
                    {
                        this.completedState = CompletedState.Canceled;
                        return;
                    }
                }

                this.completedState = CompletedState.Succeeded;
            }
        }

        private void EmitFinalProgress()
        {
            ////the last direct call to deliver most accurate and actual progress without timer
            ////without this we often have not 100% on the end of download
            if (this.completedState == CompletedState.Succeeded)
            {
                if (this.Position != this.totalBytes)
                {
                    throw new System.Exception(String.Format("Stream incomplete. Expected size: {0}, actual size {1}", this.totalBytes, this.Position));
                }

                this.OnProgressChanged(new StreamCopyProgressEventArgs { BytesReceived = Position });
            }
        }

        private void FinalizeCopyProcess(System.Exception error)
        {
            this.progressUpdateTimer.Stop();

            this.FinalizeStream(ref this.sourceStream);
            this.FinalizeStream(ref this.destinationStream);

            this.ChangeState(WorkerState.Finished);

            this.streamCopyFinished.Set();
            this.OnCompleted(new StreamCopyCompleteEventArgs { CompleteState = this.completedState, Exception = error });
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
            if (this.GetState() != WorkerState.Started)
            {
                return;
            }

            if (this.Position != this.previousReportedBytesReceived)
            {
                this.previousReportedBytesReceived = this.Position;
                this.OnProgressChanged(new StreamCopyProgressEventArgs { BytesReceived = Position });
            }
        }

        private bool ChangeState(WorkerState newState)
        {
            if (newState == WorkerState.Finished)
            {
                Interlocked.Exchange(ref this.workerState, (int)newState);
                return true;
            }
            return Interlocked.CompareExchange(ref this.workerState, (int)newState, (int)newState - 1) == (int)newState - 1;
        }

        private WorkerState GetState() => (WorkerState)this.workerState;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.progressUpdateTimer.Dispose();
                    this.streamCopyFinished.Close();
                    this.ChangeState(WorkerState.Finished);
                    this.FinalizeStream(ref this.sourceStream);
                    this.FinalizeStream(ref this.destinationStream);
                }
                this.disposed = true;
            }
        }
    }
}