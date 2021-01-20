using System;

namespace DGP.Snap.Framework.Net.Download
{
    internal class StreamCopyProgressEventArgs : EventArgs
    {
        public long BytesReceived { get; set; }
    }
}