namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Proxy;

public static class ProxyBankingTradingExample
{
    // The proxy looks like the real service to the client.
    // It decides when to create or call the expensive service.
    public static IReadOnlyList<string> Run()
    {
        IService service = new ServiceProxy();

        return new[]
        {
            "Risk report proxy",
            $"Requested risk report through a proxy.",
            service.GetData()
        };
    }

    private interface IService
    {
        string GetData();
    }

    private sealed class RiskReportService : IService
    {
        public string GetData() => "Returned the cached risk report for an approved user";
    }

    private sealed class ServiceProxy : IService
    {
        private readonly Lazy<IService> _realService = new(() => new RiskReportService());

        public string GetData() => _realService.Value.GetData();
    }
}
