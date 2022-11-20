using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using System.Text;

namespace SI.Endpoints.Core
{
    public sealed class EndpointRoutingConvention : IApplicationModelConvention
    {
        private const string Endpoint = "Endpoint";
        private readonly EndpointsConfiguration endpointsConfiguration;

        public EndpointRoutingConvention(IOptions<EndpointsConfiguration> endpointsConfiguration)
        {
            this.endpointsConfiguration = endpointsConfiguration.Value;
        }

        public void Apply(ApplicationModel application)
        {
            var controllers = application.Controllers.Where(x => x.ControllerType.IsSubclassOf(typeof(EndpointBase))).ToList();
            if (!controllers.Any())
            {
                return;
            }

            var routeBuilder = new StringBuilder();
            foreach (var controller in controllers)
            {
                routeBuilder.Append(controller.Selectors[0].AttributeRouteModel.Template);

                // replace [prefix]
                if (string.IsNullOrEmpty(endpointsConfiguration.Prefix))
                {
                    routeBuilder.Replace(EndpointBase.EndpointPrefixRouteToken, string.Empty);
                }
                else
                {
                    routeBuilder.Replace(EndpointBase.EndpointPrefixRouteToken, $"{endpointsConfiguration.Prefix}/");
                }

                // replace [feature]
                if (!endpointsConfiguration.FeaturesUsed)
                {
                    routeBuilder.Replace(EndpointBase.EndpointFeatureRouteToken, string.Empty);
                }
                else if (FeatureResolver.TryResolve(controller.ControllerType, out string? feature))
                {
                    routeBuilder.Replace(EndpointBase.EndpointFeatureRouteToken, $"{feature}/");
                }

                // replace [endpoint]
                if (endpointsConfiguration.EndpointNamesIgnored)
                {
                    routeBuilder.Replace(EndpointBase.EndpointRouteToken, string.Empty);
                }
                else
                {
                    routeBuilder.Replace(EndpointBase.EndpointRouteToken, GetControllerName(controller));
                }

                // set route and clear builder
                controller.Selectors[0].AttributeRouteModel.Template = routeBuilder.ToString();
                routeBuilder.Clear();
            }
        }

        private static string GetControllerName(ControllerModel controller)
        {
            var controllerName = controller.ControllerName;
            var endsWithEndpoint = controllerName.EndsWith(Endpoint, StringComparison.OrdinalIgnoreCase);
            if (endsWithEndpoint)
            {
                var newControllerName = controller.ControllerName.Substring(0, controller.ControllerName.Length - Endpoint.Length);
                if (!string.IsNullOrEmpty(newControllerName))
                {
                    controllerName = newControllerName;
                }
            }

            return controllerName;
        }
    }
}
