namespace SI.Endpoints
{
    public class EndpointsConfiguration
    {
        public string Prefix { get; private set; } = string.Empty;

        public bool FeaturesUsed { get; private set; }

        public bool EndpointNamesIgnored { get; private set; }

        public EndpointsConfiguration WithPrefix(string prefix)
        {
            Prefix = prefix;
            return this;
        }

        public EndpointsConfiguration UseFeatures(bool useFeatures = true)
        {
            FeaturesUsed = useFeatures;
            return this;
        }

        public EndpointsConfiguration IgnoreEndpointNames(bool ignoreEndpointNames = true)
        {
            EndpointNamesIgnored = ignoreEndpointNames;
            return this;
        }
    }
}
