namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.Builder;

public static class BuilderRealWorldExample
{
    // The director shows one readable way to build the object.
    // The builder hides the mutation details while the product stays clean.
    public static IReadOnlyList<string> Run()
    {
        var builder = new TripPlanBuilder();
        var director = new TripDirector(builder);
        director.BuildStarterPlan();
        var plan = builder.Build();

        return new[]
        {
            "Weekend trip planner",
            $"Built weekend trip: {plan.Name}",
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

    private sealed class TripPlanBuilder
    {
        private readonly Plan _plan = new();

        public void SetName(string name) => _plan.Name = name;
        public void AddStep(string step) => _plan.Steps.Add(step);
        public Plan Build() => _plan;
    }

    private sealed class TripDirector
    {
        private readonly TripPlanBuilder _builder;

        public TripDirector(TripPlanBuilder builder) => _builder = builder;

        public void BuildStarterPlan()
        {
            _builder.SetName("weekend trip");
            _builder.AddStep("Choose a mountain cabin");
            _builder.AddStep("Reserve train tickets");
            _builder.AddStep("Add a hiking checklist");
        }
    }
}
