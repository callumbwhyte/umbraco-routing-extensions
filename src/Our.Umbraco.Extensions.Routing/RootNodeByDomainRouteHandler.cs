using Our.Umbraco.Extensions.Routing.Helpers;
using System;
using Umbraco.Web;

namespace Our.Umbraco.Extensions.Routing
{
    [Obsolete("Use UmbracoVirtualRootNodeByDomainRouteHandler instead")]
    public class RootNodeByDomainRouteHandler : UmbracoVirtualRootNodeByDomainRouteHandler
    {
        public RootNodeByDomainRouteHandler(IUmbracoContextFactory contextFactory)
            : base(contextFactory)
        {

        }

        public RootNodeByDomainRouteHandler(DomainHelper domainHelper, IUmbracoContextFactory contextFactory)
            : base(domainHelper, contextFactory)
        {

        }
    }
}