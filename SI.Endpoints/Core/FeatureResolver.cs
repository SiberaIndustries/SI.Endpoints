using System.Linq;
using System.Reflection;

namespace SI.Endpoints.Core
{
    public static class FeatureResolver
    {
        public static bool TryResolve(TypeInfo value, out string? feature)
        {
            feature = value.Namespace.Split('.').LastOrDefault();
            return feature != null;
        }
    }
}
