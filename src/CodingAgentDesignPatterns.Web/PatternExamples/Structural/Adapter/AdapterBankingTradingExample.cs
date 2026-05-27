namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Adapter;

public static class AdapterBankingTradingExample
{
    // The client expects the target interface.
    // The adapter translates calls to the existing class without changing the old class itself.
    public static IReadOnlyList<string> Run()
    {
        ITarget target = new Adapter(new LegacyService());

        return new[]
        {
            "Legacy price feed adapter",
            $"Client speaks to the pricing screen.",
            target.Request()
        };
    }

    private interface ITarget
    {
        string Request();
    }

    private sealed class LegacyService
    {
        public string SpecificRequest() => "EUR/USD delivered as a normalized price";
    }

    private sealed class Adapter : ITarget
    {
        private readonly LegacyService _legacyService;

        public Adapter(LegacyService legacyService) => _legacyService = legacyService;

        public string Request() => $"Wrapped legacy feed gateway -> {_legacyService.SpecificRequest()}";
    }
}
