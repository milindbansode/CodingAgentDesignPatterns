namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.Singleton;

public static class SingletonRealWorldExample
{
    // The singleton exposes one shared instance.
    // Every caller reads the same dictionary, so the state stays consistent.
    public static IReadOnlyList<string> Run()
    {
        var store = AppSettingsStore.Instance;
        store.Set("Theme", "Dark");

        return new[]
        {
            "App settings store",
            $"Shared instance type: {store.GetType().Name}",
            $"Theme = {store.Get("Theme")}"
        };
    }

    private sealed class AppSettingsStore
    {
        private readonly Dictionary<string, string> _values = new();

        private AppSettingsStore()
        {
        }

        public static AppSettingsStore Instance { get; } = new();

        public void Set(string key, string value) => _values[key] = value;

        public string Get(string key) => _values.TryGetValue(key, out var value) ? value : "Missing";
    }
}
