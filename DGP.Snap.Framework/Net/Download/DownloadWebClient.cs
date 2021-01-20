using DGP.Snap.Framework.Extensions;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace DGP.Snap.Framework.Net.Download
{
    [DesignerCategory("Code")]
    internal class DownloadWebClient : WebClient
    {
        private readonly CookieContainer cookieContainer = new CookieContainer();
        private WebResponse webResponse;
        private long position;

        private TimeSpan timeout = TimeSpan.FromMinutes(2);

        public bool HasResponse => this.webResponse != null;

        public bool IsPartialResponse => this.webResponse is HttpWebResponse response && response.StatusCode == HttpStatusCode.PartialContent;

        public void OpenReadAsync(Uri address, long newPosition)
        {
            this.position = newPosition;
            this.OpenReadAsync(address);
        }

        public string GetOriginalFileNameFromDownload()
        {
            if (this.webResponse == null)
            {
                return null;
            }

            try
            {
                var contentDisposition = this.webResponse.Headers.GetContentDisposition();
                if (contentDisposition != null)
                {
                    var filename = contentDisposition.FileName;
                    if (!String.IsNullOrEmpty(filename))
                    {
                        return Path.GetFileName(filename);
                    }
                }
                return Path.GetFileName(this.webResponse.ResponseUri.LocalPath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = base.GetWebResponse(request);
            this.webResponse = response;
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            var response = base.GetWebResponse(request, result);
            this.webResponse = response;

            return response;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);

            if (request != null)
            {
                request.Timeout = (int)this.timeout.TotalMilliseconds;
            }

            var webRequest = request as HttpWebRequest;
            if (webRequest == null)
            {
                return request;
            }

            webRequest.ReadWriteTimeout = (int)this.timeout.TotalMilliseconds;
            webRequest.Timeout = (int)this.timeout.TotalMilliseconds;
            if (this.position != 0)
            {
                webRequest.AddRange((int)this.position);
                webRequest.Accept = "*/*";
            }
            webRequest.CookieContainer = this.cookieContainer;
            return request;
        }
    }
}