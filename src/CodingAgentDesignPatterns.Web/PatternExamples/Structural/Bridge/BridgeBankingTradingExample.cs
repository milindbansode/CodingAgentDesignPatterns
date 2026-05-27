namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Bridge;

public static class BridgeBankingTradingExample
{
    // The abstraction delegates work to the implementation.
    // Either side can change without forcing the other side to multiply subclasses.
    public static IReadOnlyList<string> Run()
    {
        var abstraction = new RefinedAbstraction(new ConcreteImplementor());

        return new[]
        {
            "Risk report and delivery channel",
            $"Abstraction: risk report",
            abstraction.Operation()
        };
    }

    private interface IImplementor
    {
        string Execute();
    }

    private sealed class ConcreteImplementor : IImplementor
    {
        public string Execute() => "Risk summary published to the dashboard via the dashboard channel.";
    }

    private abstract class Abstraction
    {
        protected Abstraction(IImplementor implementor) => Implementor = implementor;

        protected IImplementor Implementor { get; }

        public abstract string Operation();
    }

    private sealed class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(IImplementor implementor) : base(implementor)
        {
        }

        public override string Operation() => Implementor.Execute();
    }
}
