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

        public bool HasResponse => webResponse != null;

        public bool IsPartialResponse => webResponse is HttpWebResponse response && response.StatusCode == HttpStatusCode.PartialContent;

        public void OpenReadAsync(Uri address, long newPosition)
        {
            position = newPosition;
            OpenReadAsync(address);
        }

        public string GetOriginalFileNameFromDownload()
        {
            if (webResponse == null)
            {
                return null;
            }

            try
            {
                var contentDisposition = webResponse.Headers.GetContentDisposition();
                if (contentDisposition != null)
                {
                    var filename = contentDisposition.FileName;
                    if (!String.IsNullOrEmpty(filename))
                    {
                        return Path.GetFileName(filename);
                    }
                }
                return Path.GetFileName(webResponse.ResponseUri.LocalPath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = base.GetWebResponse(request);
            webResponse = response;
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            var response = base.GetWebResponse(request, result);
            webResponse = response;

            return response;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);

            if (request != null)
            {
                request.Timeout = (int)timeout.TotalMilliseconds;
            }

            var webRequest = request as HttpWebRequest;
            if (webRequest == null)
            {
                return request;
            }

            webRequest.ReadWriteTimeout = (int)timeout.TotalMilliseconds;
            webRequest.Timeout = (int)timeout.TotalMilliseconds;
            if (position != 0)
            {
                webRequest.AddRange((int)position);
                webRequest.Accept = "*/*";
            }
            webRequest.CookieContainer = cookieContainer;
            return request;
        }
    }
}