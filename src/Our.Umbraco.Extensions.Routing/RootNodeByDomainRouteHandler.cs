using System.Linq;
using System.Web.Routing;
using Our.Umbraco.Extensions.Routing.Helpers;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.Extensions.Routing
{
    public class RootNodeByDomainRouteHandler : UmbracoVirtualNodeRouteHandler
    {
        private readonly DomainHelper _domainHelper;
        private readonly IUmbracoContextFactory _contextFactory;

        public RootNodeByDomainRouteHandler(DomainHelper domainHelper, IUmbracoContextFactory contextFactory)
        {
            _domainHelper = domainHelper;
            _contextFactory = contextFactory;
        }

        protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            var url = requestContext.HttpContext.Request.UrlOrForwarded();

            var domain = _domainHelper.GetDomainByUri(umbracoContext, url);

            if (domain == null)
            {
                // No domains are configured, use the first root node which can be found
                return _umbracoContextFactory.EnsureUmbracoContext().UmbracoContext.Content.GetAtRoot().FirstOrDefault();
            }

            return _domainHelper.GetContentByDomain(umbracoContext, domain);
        }

        protected override UmbracoContext GetUmbracoContext(RequestContext requestContext)
        {
            var umbracoContext = base.GetUmbracoContext(requestContext);

            if (umbracoContext == null)
            {
                var contextReference = _contextFactory.EnsureUmbracoContext(requestContext.HttpContext);

                return contextReference.UmbracoContext;
            }

            return umbracoContext;
        }
    }
}