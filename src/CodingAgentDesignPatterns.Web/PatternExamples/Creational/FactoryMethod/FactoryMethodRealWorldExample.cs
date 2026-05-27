namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.FactoryMethod;

public static class FactoryMethodRealWorldExample
{
    // The workflow knows the common steps.
    // The factory method lets a subclass decide which concrete worker to create.
    public static IReadOnlyList<string> Run()
    {
        var workflow = new SmsNotificationWorkflow();
        var result = workflow.Execute();

        return new[]
        {
            "Notification sender",
            $"Created SMS notification through the factory method.",
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

    private sealed class SmsNotificationWorkflow : Workflow
    {
        protected override IWorker CreateWorker() => new SmsSender();
    }

    private sealed class SmsSender : IWorker
    {
        public string Process() => "Sent a pickup reminder by SMS";
    }
}
