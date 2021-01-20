using System;

namespace DGP.Snap.Framework.Net.Download
{
    /// <summary>
    /// DownloadFileCompleted event args
    /// </summary>
    public class DownloadFileCompletedArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileCompletedArgs"/> class
        /// </summary>
        /// <param name="state">State of download</param>
        /// <param name="fileName">Local path to downloaded file</param>
        /// <param name="fileSource">Downloaded file source</param>
        /// <param name="downloadTime">Downloaded time</param>
        /// <param name="bytesTotal">File size</param>
        /// <param name="bytesReceived">Received bytes</param>
        /// <param name="error">Exception object</param>
        public DownloadFileCompletedArgs(CompletedState state, string fileName, Uri fileSource, TimeSpan downloadTime, long bytesTotal, long bytesReceived, System.Exception error)
        {
            this.State = state;
            this.FileName = fileName;
            this.FileSource = fileSource;
            this.Error = error;
            this.DownloadTime = downloadTime;
            this.BytesTotal = bytesTotal;
            this.BytesReceived = bytesReceived;
        }

        /// <summary>
        /// Gets the download state 
        /// </summary>
        public CompletedState State { get; private set; }

        /// <summary>
        /// Gets the name of downloaded file
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the download source
        /// </summary>
        public Uri FileSource { get; private set; }

        /// <summary>
        /// Gets the error, or null if there is no error
        /// </summary>
        public System.Exception Error { get; private set; }

        /// <summary>
        /// Gets the total download time
        /// </summary>
        public TimeSpan DownloadTime { get; private set; }

        /// <summary>
        /// Gets the number of received bytes
        /// </summary>
        public long BytesReceived { get; private set; }

        /// <summary>
        /// Gets the number of total bytes which should be received
        /// </summary>
        public long BytesTotal { get; private set; }

        /// <summary>
        /// Gets the download progress in percent, from 0 to 100
        /// </summary>
        public int DownloadProgress
        {
            get
            {
                if (this.BytesTotal <= 0 || this.BytesReceived <= 0)
                {
                    return 0;
                }
                return Convert.ToInt32((float)this.BytesReceived / this.BytesTotal * 100);
            }
        }

        /// <summary>
        /// Gets the download speed in kilobytes per second
        /// </summary>
        public int DownloadSpeedInKiloBytesPerSecond
        {
            get
            {
                if (this.DownloadTime == TimeSpan.Zero || this.BytesReceived == 0)
                {
                    return 0;
                }

                var kiloBytesReceived = this.BytesReceived / 1024.0;

                return Convert.ToInt32(kiloBytesReceived / this.DownloadTime.TotalSeconds);
            }
        }
    }
}