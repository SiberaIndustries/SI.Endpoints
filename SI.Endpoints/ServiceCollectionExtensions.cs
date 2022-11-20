using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SI.Endpoints.Core;

namespace SI.Endpoints
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEndpointsRouting(this IServiceCollection services, Action<EndpointsConfiguration>? configure = null)
        {
            services.AddSingleton<EndpointRoutingConvention>();
            services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureMvcOptionsForEndpoints>();
            if (configure != null)
            {
                services.Configure(configure);
            }

            return services;
        }
    }
}
