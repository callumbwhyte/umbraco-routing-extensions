using System.Web.Routing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.Extensions.Routing
{
    public class UmbracoVirtualNodeByXPathRouteHandler : UmbracoVirtualNodeRouteHandler
    {
        private readonly string _xpath;

        public UmbracoVirtualNodeByXPathRouteHandler(string xpath)
        {
            _xpath = xpath;
        }

        protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            return umbracoContext?.Content.GetSingleByXPath(_xpath);
        }
    }
}