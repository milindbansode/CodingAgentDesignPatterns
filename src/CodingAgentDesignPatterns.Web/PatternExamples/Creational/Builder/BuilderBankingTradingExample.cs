namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.Builder;

public static class BuilderBankingTradingExample
{
    // The director shows one readable way to build the object.
    // The builder hides the mutation details while the product stays clean.
    public static IReadOnlyList<string> Run()
    {
        var builder = new TradeTicketBuilder();
        var director = new TradeTicketDirector(builder);
        director.BuildStarterPlan();
        var plan = builder.Build();

        return new[]
        {
            "Trade ticket builder",
            $"Built trade ticket: {plan.Name}",
            $"Step 1: {plan.Steps[0]}",
            $"Step 2: {plan.Steps[1]}",
            $"Step 3: {plan.Steps[2]}"
        };
    }

    private sealed class Plan
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Steps { get; } = new();
    }

    private sealed class TradeTicketBuilder
    {
        private readonly Plan _plan = new();

        public void SetName(string name) => _plan.Name = name;
        public void AddStep(string step) => _plan.Steps.Add(step);
        public Plan Build() => _plan;
    }

    private sealed class TradeTicketDirector
    {
        private readonly TradeTicketBuilder _builder;

        public TradeTicketDirector(TradeTicketBuilder builder) => _builder = builder;

        public void BuildStarterPlan()
        {
            _builder.SetName("trade ticket");
            _builder.AddStep("Select an equity instrument");
            _builder.AddStep("Add quantity and limit price");
            _builder.AddStep("Attach a compliance note");
        }
    }
}
