using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SI.Endpoints.Core
{
    internal sealed class ConfigureMvcOptionsForEndpoints : IConfigureOptions<MvcOptions>
    {
        private readonly EndpointRoutingConvention endpointRoutingConvention;

        public ConfigureMvcOptionsForEndpoints(EndpointRoutingConvention endpointRoutingConvention)
        {
            this.endpointRoutingConvention = endpointRoutingConvention;
        }

        public void Configure(MvcOptions options)
        {
            options.Conventions.Add(endpointRoutingConvention);
        }
    }
}
