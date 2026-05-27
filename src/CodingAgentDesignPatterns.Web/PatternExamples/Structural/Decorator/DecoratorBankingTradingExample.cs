namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Decorator;

public static class DecoratorBankingTradingExample
{
    // The component starts simple.
    // Each decorator wraps the previous object and adds one more behavior or description.
    public static IReadOnlyList<string> Run()
    {
        IComponent component = new BaseComponent();
        component = new ComplianceDecorator(component);
        component = new FeePreviewDecorator(component);

        return new[]
        {
            "Order ticket enrichment",
            $"Base item: order ticket",
            component.Describe()
        };
    }

    private interface IComponent
    {
        string Describe();
    }

    private sealed class BaseComponent : IComponent
    {
        public string Describe() => "order ticket";
    }

    private abstract class Decorator : IComponent
    {
        protected Decorator(IComponent inner) => Inner = inner;

        protected IComponent Inner { get; }

        public abstract string Describe();
    }

    private sealed class ComplianceDecorator : Decorator
    {
        public ComplianceDecorator(IComponent inner) : base(inner)
        {
        }

        public override string Describe() => $"{Inner.Describe()} + ComplianceDecorator";
    }

    private sealed class FeePreviewDecorator : Decorator
    {
        public FeePreviewDecorator(IComponent inner) : base(inner)
        {
        }

        public override string Describe() => "Ticket + compliance check + fee preview";
    }
}
