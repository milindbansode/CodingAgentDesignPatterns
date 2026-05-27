namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Flyweight;

public static class FlyweightRealWorldExample
{
    // The shared object stores intrinsic state once.
    // Each placement object keeps only the unique state that changes per usage.
    public static IReadOnlyList<string> Run()
    {
        var factory = new FlyweightFactory();
        var shared = factory.GetShared("Oak");
        var first = new Placement(shared, "(10, 20)");
        var second = new Placement(shared, "(18, 26)");

        return new[]
        {
            "Park tree map",
            $"Shared object reused: {ReferenceEquals(first.Shared, second.Shared)}",
            first.Describe(),
            second.Describe()
        };
    }

    private sealed class SharedData
    {
        public SharedData(string name) => Name = name;

        public string Name { get; }
    }

    private sealed class Placement
    {
        public Placement(SharedData shared, string uniqueState)
        {
            Shared = shared;
            UniqueState = uniqueState;
        }

        public SharedData Shared { get; }
        public string UniqueState { get; }

        public string Describe() => $"{Shared.Name} placed at {UniqueState}";
    }

    private sealed class FlyweightFactory
    {
        private readonly Dictionary<string, SharedData> _cache = new();

        public SharedData GetShared(string key)
        {
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = new SharedData(key);
            }

            return _cache[key];
        }
    }
}
