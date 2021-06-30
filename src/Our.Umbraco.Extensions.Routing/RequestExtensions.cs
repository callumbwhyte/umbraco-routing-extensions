using System;
using System.Web;

namespace Our.Umbraco.Extensions.Routing
{
    public static class RequestExtensions
    {
        public static Uri UrlOrForwarded(this HttpRequestBase request)
        {
            var forwardedHost = request.Headers.Get("X-Forwarded-Host");

            if (string.IsNullOrWhiteSpace(forwardedHost) == true)
            {
                return request.Url;
            }

            var forwardedProtocol = request.Headers.Get("X-Forwarded-Proto");

            if (string.IsNullOrWhiteSpace(forwardedProtocol) == true)
            {
                forwardedProtocol = "http";
            }

            var forwardedUriString = $"{forwardedProtocol}://{forwardedHost}";

            Uri.TryCreate(forwardedUriString, UriKind.Absolute, out Uri forwardedUri);

            return forwardedUri;
        }
    }
}