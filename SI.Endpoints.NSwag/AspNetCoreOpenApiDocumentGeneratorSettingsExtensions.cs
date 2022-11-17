using NSwag.Generation.AspNetCore;

namespace SI.Endpoints
{
    public static class AspNetCoreOpenApiDocumentGeneratorSettingsExtensions
    {
        public static void EnableFeatureFilter(this AspNetCoreOpenApiDocumentGeneratorSettings settings, bool replaceExistingTags = true)
        {
            settings.OperationProcessors.Add(new FeatureFilter(replaceExistingTags));
        }
    }
}
