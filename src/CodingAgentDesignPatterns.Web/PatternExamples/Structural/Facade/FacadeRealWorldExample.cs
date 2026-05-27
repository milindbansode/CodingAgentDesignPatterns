namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Facade;

public static class FacadeRealWorldExample
{
    // The facade exposes one simple method.
    // Inside that method, it coordinates the smaller subsystem classes in the correct order.
    public static IReadOnlyList<string> Run()
    {
        var facade = new TripBookingFacade();
        return facade.CompleteWorkflow();
    }

    private sealed class TripBookingFacade
    {
        public IReadOnlyList<string> CompleteWorkflow()
        {
            var steps = new List<string>
            {
                "Trip booking facade",
                new StepOne().Run(),
                new StepTwo().Run(),
                new StepThree().Run()
            };

            return steps;
        }
    }

    private sealed class StepOne
    {
        public string Run() => "Flight booked";
    }

    private sealed class StepTwo
    {
        public string Run() => "Hotel reserved";
    }

    private sealed class StepThree
    {
        public string Run() => "Airport cab scheduled";
    }
}
