namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Proxy;

public static class ProxyRealWorldExample
{
    // The proxy looks like the real service to the client.
    // It decides when to create or call the expensive service.
    public static IReadOnlyList<string> Run()
    {
        IService service = new ServiceProxy();

        return new[]
        {
            "Image loader proxy",
            $"Requested gallery image through a proxy.",
            service.GetData()
        };
    }

    private interface IService
    {
        string GetData();
    }

    private sealed class HighResolutionImage : IService
    {
        public string GetData() => "Loaded poster only when requested";
    }

    private sealed class ServiceProxy : IService
    {
        private readonly Lazy<IService> _realService = new(() => new HighResolutionImage());

        public string GetData() => _realService.Value.GetData();
    }
}
