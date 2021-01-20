using System.Net;
using System.Net.Mime;

namespace DGP.Snap.Framework.Extensions
{
    public static class WebHeaderCollectionExtensions
    {
        public static long GetContentLength(this WebHeaderCollection responseHeaders)
        {
            long contentLength = -1;
            if (responseHeaders != null && !System.String.IsNullOrEmpty(responseHeaders["Content-Length"]))
            {
                System.Int64.TryParse(responseHeaders["Content-Length"], out contentLength);
            }
            return contentLength;
        }

        public static ContentDisposition GetContentDisposition(this WebHeaderCollection responseHeaders)
        {
            if (responseHeaders != null && !System.String.IsNullOrEmpty(responseHeaders["Content-Disposition"]))
            {
                return new ContentDisposition(responseHeaders["Content-Disposition"]);
            }
            return null;
        }
    }
}
