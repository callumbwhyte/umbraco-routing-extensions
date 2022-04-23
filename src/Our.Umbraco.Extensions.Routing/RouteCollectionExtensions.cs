using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.Extensions.Routing
{
    public static class RouteCollectionExtensions
    {
        public static Route MapSurfaceRoute(this RouteCollection routes, string name, string url, object defaults, UmbracoVirtualNodeRouteHandler virtualNodeHandler = null, object constraints = null, string[] namespaces = null)
        {
            var routeHandler = new SurfaceVirtualNodeRouteHandler(virtualNodeHandler);

            var route = routes.MapUmbracoRoute(name, url, defaults, routeHandler, constraints, namespaces);

            if (route.DataTokens.ContainsKey(Constants.Web.UmbracoDataToken) == false)
            {
                route.DataTokens[Constants.Web.UmbracoDataToken] = "renderMvc";
            }

            return route;
        }
    }
}