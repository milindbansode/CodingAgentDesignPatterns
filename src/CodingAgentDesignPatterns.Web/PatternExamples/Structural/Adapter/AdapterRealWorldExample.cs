namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Adapter;

public static class AdapterRealWorldExample
{
    // The client expects the target interface.
    // The adapter translates calls to the existing class without changing the old class itself.
    public static IReadOnlyList<string> Run()
    {
        ITarget target = new Adapter(new LegacyService());

        return new[]
        {
            "Smart plug adapter",
            $"Client speaks to the wall socket.",
            target.Request()
        };
    }

    private interface ITarget
    {
        string Request();
    }

    private sealed class LegacyService
    {
        public string SpecificRequest() => "Lamp powered through the adapter";
    }

    private sealed class Adapter : ITarget
    {
        private readonly LegacyService _legacyService;

        public Adapter(LegacyService legacyService) => _legacyService = legacyService;

        public string Request() => $"Wrapped USB desk lamp -> {_legacyService.SpecificRequest()}";
    }
}
