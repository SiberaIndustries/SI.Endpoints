using Microsoft.AspNetCore.Mvc;

namespace SI.Endpoints.Core
{
    [ApiController]
    [Route(EndpointRoute)]
    public abstract class EndpointBase : ControllerBase
    {
        internal const string EndpointPrefixRouteToken = "[prefix]";
        internal const string EndpointFeatureRouteToken = "[feature]";
        internal const string EndpointRouteToken = "[endpoint]";
        internal const string EndpointRoute = EndpointPrefixRouteToken + EndpointFeatureRouteToken + EndpointRouteToken;
    }
}
