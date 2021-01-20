using System;
using System.IO;

namespace DGP.Snap.Framework.Net.Download
{
    internal interface IStreamCopyWorker
    {
        event EventHandler<StreamCopyCompleteEventArgs> Completed;

        event EventHandler<StreamCopyProgressEventArgs> ProgressChanged;

        long Position { get; }

        void CopyAsync(Stream source, Stream destination, long sizeInBytes);

        void Cancel();
    }
}