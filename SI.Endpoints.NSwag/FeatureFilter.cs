using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using SI.Endpoints.Core;
using System.Linq;
using System.Reflection;

namespace SI.Endpoints
{
    public class FeatureFilter : IOperationProcessor
    {
        private readonly bool replaceExistingTags = true;

        public FeatureFilter()
        {
        }

        public FeatureFilter(bool replaceExistingTags)
        {
            this.replaceExistingTags = replaceExistingTags;
        }

        public bool Process(OperationProcessorContext context)
        {
            if (context.MethodInfo.DeclaringType.IsSubclassOf(typeof(EndpointBase)) &&
                (context.MethodInfo.Name == "HandleAsync" || context.MethodInfo.Name == "Handle") &&
                FeatureResolver.TryResolve(context.MethodInfo.DeclaringType.GetTypeInfo(), out string? feature))
            {
                if (replaceExistingTags || context.OperationDescription.Operation.Tags == null)
                {
                    context.OperationDescription.Operation.Tags = new[] { feature }.ToList();
                }
                else
                {
                    context.OperationDescription.Operation.Tags.Add(feature);
                }
            }

            return true;
        }
    }
}
