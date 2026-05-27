namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.Singleton;

public static class SingletonBankingTradingExample
{
    // The singleton exposes one shared instance.
    // Every caller reads the same dictionary, so the state stays consistent.
    public static IReadOnlyList<string> Run()
    {
        var store = MarketHoursCalendar.Instance;
        store.Set("NYSE", "09:30-16:00");

        return new[]
        {
            "Market hours calendar",
            $"Shared instance type: {store.GetType().Name}",
            $"NYSE = {store.Get("NYSE")}"
        };
    }

    private sealed class MarketHoursCalendar
    {
        private readonly Dictionary<string, string> _values = new();

        private MarketHoursCalendar()
        {
        }

        public static MarketHoursCalendar Instance { get; } = new();

        public void Set(string key, string value) => _values[key] = value;

        public string Get(string key) => _values.TryGetValue(key, out var value) ? value : "Missing";
    }
}
