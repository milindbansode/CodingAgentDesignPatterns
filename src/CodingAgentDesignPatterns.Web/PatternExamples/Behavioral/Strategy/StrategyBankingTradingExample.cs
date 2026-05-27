namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Strategy;

public static class StrategyBankingTradingExample
{
    // The context delegates the algorithm to a strategy object.
    // Swapping strategies changes the behavior without rewriting the context.
    public static IReadOnlyList<string> Run()
    {
        var context = new FeeCalculator(new InstitutionalFeeStrategy());

        return new[]
        {
            "Fee calculation strategy",
            context.Execute()
        };
    }

    private interface IStrategy
    {
        string Run();
    }

    private sealed class FeeCalculator
    {
        private readonly IStrategy _strategy;

        public FeeCalculator(IStrategy strategy) => _strategy = strategy;

        public string Execute() => _strategy.Run();
    }

    private sealed class InstitutionalFeeStrategy : IStrategy
    {
        public string Run() => "Institutional fee calculated";
    }
}
