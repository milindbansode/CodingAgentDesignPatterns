namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Facade;

public static class FacadeBankingTradingExample
{
    // The facade exposes one simple method.
    // Inside that method, it coordinates the smaller subsystem classes in the correct order.
    public static IReadOnlyList<string> Run()
    {
        var facade = new TradeExecutionFacade();
        return facade.CompleteWorkflow();
    }

    private sealed class TradeExecutionFacade
    {
        public IReadOnlyList<string> CompleteWorkflow()
        {
            var steps = new List<string>
            {
                "Trade execution facade",
                new StepOne().Run(),
                new StepTwo().Run(),
                new StepThree().Run()
            };

            return steps;
        }
    }

    private sealed class StepOne
    {
        public string Run() => "Limits checked";
    }

    private sealed class StepTwo
    {
        public string Run() => "Trade booked";
    }

    private sealed class StepThree
    {
        public string Run() => "Client confirmation sent";
    }
}
