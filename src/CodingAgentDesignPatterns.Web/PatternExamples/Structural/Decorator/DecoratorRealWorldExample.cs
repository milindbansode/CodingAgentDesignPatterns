namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Decorator;

public static class DecoratorRealWorldExample
{
    // The component starts simple.
    // Each decorator wraps the previous object and adds one more behavior or description.
    public static IReadOnlyList<string> Run()
    {
        IComponent component = new BaseComponent();
        component = new MilkDecorator(component);
        component = new SugarDecorator(component);

        return new[]
        {
            "Coffee customizer",
            $"Base item: coffee",
            component.Describe()
        };
    }

    private interface IComponent
    {
        string Describe();
    }

    private sealed class BaseComponent : IComponent
    {
        public string Describe() => "coffee";
    }

    private abstract class Decorator : IComponent
    {
        protected Decorator(IComponent inner) => Inner = inner;

        protected IComponent Inner { get; }

        public abstract string Describe();
    }

    private sealed class MilkDecorator : Decorator
    {
        public MilkDecorator(IComponent inner) : base(inner)
        {
        }

        public override string Describe() => $"{Inner.Describe()} + MilkDecorator";
    }

    private sealed class SugarDecorator : Decorator
    {
        public SugarDecorator(IComponent inner) : base(inner)
        {
        }

        public override string Describe() => "Coffee + milk + sugar";
    }
}
