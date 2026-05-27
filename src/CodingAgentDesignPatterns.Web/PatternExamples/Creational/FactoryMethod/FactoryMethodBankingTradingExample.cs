namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.FactoryMethod;

public static class FactoryMethodBankingTradingExample
{
    // The workflow knows the common steps.
    // The factory method lets a subclass decide which concrete worker to create.
    public static IReadOnlyList<string> Run()
    {
        var workflow = new FxOrderWorkflow();
        var result = workflow.Execute();

        return new[]
        {
            "Order processor selector",
            $"Created FX order processor through the factory method.",
            result
        };
    }

    private abstract class Workflow
    {
        public string Execute()
        {
            var worker = CreateWorker();
            return worker.Process();
        }

        protected abstract IWorker CreateWorker();
    }

    private interface IWorker
    {
        string Process();
    }

    private sealed class FxOrderWorkflow : Workflow
    {
        protected override IWorker CreateWorker() => new FxOrderProcessor();
    }

    private sealed class FxOrderProcessor : IWorker
    {
        public string Process() => "Booked an FX spot order";
    }
}
