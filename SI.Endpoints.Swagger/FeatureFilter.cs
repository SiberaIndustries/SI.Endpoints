using Microsoft.OpenApi.Models;
using SI.Endpoints.Core;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SI.Endpoints
{
    public class FeatureFilter : IOperationFilter
    {
        private readonly bool replaceExistingTags = true;

        public FeatureFilter(bool replaceExistingTags = true)
        {
            this.replaceExistingTags = replaceExistingTags;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.DeclaringType.IsSubclassOf(typeof(EndpointBase)) &&
                (context.MethodInfo.Name == "HandleAsync" || context.MethodInfo.Name == "Handle") &&
                FeatureResolver.TryResolve(context.MethodInfo.DeclaringType.GetTypeInfo(), out string? feature))
            {
                if (replaceExistingTags || operation.Tags == null)
                {
                    operation.Tags = new[] { new OpenApiTag { Name = feature } };
                }
                else
                {
                    operation.Tags.Add(new OpenApiTag { Name = feature });
                }
            }
        }
    }
}
