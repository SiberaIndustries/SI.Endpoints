using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SI.Endpoints
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void EnableFeatureFilter(this SwaggerGenOptions options, bool replaceExistingTags = true)
        {
            options.OperationFilter<FeatureFilter>(replaceExistingTags);
        }
    }
}
