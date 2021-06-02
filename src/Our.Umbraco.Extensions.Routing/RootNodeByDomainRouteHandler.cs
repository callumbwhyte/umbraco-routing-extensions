using System;
using System.Linq;
using System.Web.Routing;

using Our.Umbraco.Extensions.Routing.Helpers;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.Extensions.Routing
{
    public class RootNodeByDomainRouteHandler : UmbracoVirtualNodeRouteHandler
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public RootNodeByDomainRouteHandler(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }

        protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            var requestUri = requestContext.HttpContext.Request.Url;

            var domain = DomainHelper.GetDomainByUri(umbracoContext, requestUri);

            if (domain == null)
            {
                var forwardedHost = requestContext.HttpContext.Request.Headers.Get("X-Forwarded-Host");
                var isHttps = requestContext.HttpContext.Request.Headers.Get("X-Forwarded-Proto")
                    .InvariantEquals("HTTPS");

                var forwardedUriString = isHttps ? "https" : "http";
                forwardedUriString += $"://{forwardedHost}";

                if (!string.IsNullOrWhiteSpace(forwardedHost) && Uri.TryCreate(
                    forwardedUriString,
                    UriKind.Absolute,
                    out var forwardedUri))
                {
                    domain = DomainHelper.GetDomainByUri(umbracoContext, forwardedUri);
                }
                else
                {
                    // No domains are configured, use the first root node which can be found
                    return _umbracoContextFactory.EnsureUmbracoContext().UmbracoContext.Content.GetAtRoot().FirstOrDefault();
                }
            }

            var content = DomainHelper.GetContentByDomain(umbracoContext, domain);

            if (content == null)
            {
                return content;
            }

            return content;
        }

        protected override UmbracoContext GetUmbracoContext(RequestContext requestContext)
        {
            var umbracoContext = base.GetUmbracoContext(requestContext);

            if (umbracoContext == null)
            {
                var contextReference = _umbracoContextFactory.EnsureUmbracoContext(requestContext.HttpContext);

                return contextReference.UmbracoContext;
            }

            return umbracoContext;
        }
    }
}
