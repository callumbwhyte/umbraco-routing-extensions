using System;
using System.Web;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.Routing;

namespace Our.Umbraco.Extensions.Routing
{
    internal class SurfaceVirtualNodeRouteHandler : UmbracoVirtualNodeRouteHandler, IRouteHandler
    {
        private readonly UmbracoVirtualNodeRouteHandler _routeHandler;

        public SurfaceVirtualNodeRouteHandler(UmbracoVirtualNodeRouteHandler routeHandler = null)
        {
            _routeHandler = routeHandler ?? this;
        }

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            // reset Umbraco route data tokens
            ResetUmbracoDataTokens(requestContext);

            // get the HTTP handler from the route handler
            var httpHandler = _routeHandler.GetHttpHandler(requestContext);

            // ensure the published request is set
            EnsurePublishedRequest(requestContext);

            // ensure the route definition is set to the controller
            EnsureRouteDefinition(requestContext);

            return httpHandler;
        }

        protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            throw new NotImplementedException();
        }

        private void ResetUmbracoDataTokens(RequestContext requestContext)
        {
            requestContext.RouteData.DataTokens.Remove(Constants.Web.UmbracoDataToken);
            requestContext.RouteData.DataTokens.Remove(Constants.Web.PublishedDocumentRequestDataToken);
            requestContext.RouteData.DataTokens.Remove(Constants.Web.UmbracoContextDataToken);
            requestContext.RouteData.DataTokens.Remove(Constants.Web.CustomRouteDataToken);
        }

        private UmbracoContext EnsureUmbracoContext(RequestContext requestContext)
        {
            var key = Constants.Web.UmbracoContextDataToken;

            if (requestContext.RouteData.DataTokens.ContainsKey(key) == false)
            {
                requestContext.RouteData.DataTokens[key] = GetUmbracoContext(requestContext);
            }

            return (UmbracoContext)requestContext.RouteData.DataTokens[key];
        }

        private PublishedRequest EnsurePublishedRequest(RequestContext requestContext)
        {
            var key = Constants.Web.PublishedDocumentRequestDataToken;

            if (requestContext.RouteData.DataTokens.ContainsKey(key) == false)
            {
                var umbracoContext = EnsureUmbracoContext(requestContext);

                requestContext.RouteData.DataTokens[key] = umbracoContext.PublishedRequest;
            }

            return (PublishedRequest)requestContext.RouteData.DataTokens[key];
        }

        private RouteDefinition EnsureRouteDefinition(RequestContext requestContext)
        {
            var key = Constants.Web.UmbracoRouteDefinitionDataToken;

            if (requestContext.RouteData.DataTokens.ContainsKey(key) == false)
            {
                var publishedRequest = EnsurePublishedRequest(requestContext);

                requestContext.RouteData.DataTokens[key] = new RouteDefinition
                {
                    ActionName = requestContext.RouteData.GetRequiredString("action"),
                    ControllerName = requestContext.RouteData.GetRequiredString("controller"),
                    PublishedRequest = publishedRequest
                };
            }

            return (RouteDefinition)requestContext.RouteData.DataTokens[key];
        }
    }
}