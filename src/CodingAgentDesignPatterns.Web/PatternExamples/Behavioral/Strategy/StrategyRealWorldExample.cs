namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Strategy;

public static class StrategyRealWorldExample
{
    // The context delegates the algorithm to a strategy object.
    // Swapping strategies changes the behavior without rewriting the context.
    public static IReadOnlyList<string> Run()
    {
        var context = new RoutePlanner(new WalkingStrategy());

        return new[]
        {
            "Route planning strategy",
            context.Execute()
        };
    }

    private interface IStrategy
    {
        string Run();
    }

    private sealed class RoutePlanner
    {
        private readonly IStrategy _strategy;

        public RoutePlanner(IStrategy strategy) => _strategy = strategy;

        public string Execute() => _strategy.Run();
    }

    private sealed class WalkingStrategy : IStrategy
    {
        public string Run() => "Route planned for walking";
    }
}
