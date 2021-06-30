using Our.Umbraco.Extensions.Routing.Helpers;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Our.Umbraco.Extensions.Routing.Startup
{
    public class RoutingComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.RegisterUnique<DomainHelper>();
        }
    }
}