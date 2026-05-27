namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.AbstractFactory;

public static class AbstractFactoryBankingTradingExample
{
    // This example keeps the client code simple.
    // The client chooses one factory and receives a matching pair of related objects.
    public static IReadOnlyList<string> Run()
    {
        IBundleFactory factory = new InstitutionalTradingFactory();
        var primary = factory.CreatePrimary();
        var secondary = factory.CreateSecondary();

        return new[]
        {
            "Trading desk dashboard kit",
            $"Selected trading dashboard kit: {factory.Name}",
            primary.Describe(),
            secondary.Describe()
        };
    }

    private interface IBundleFactory
    {
        string Name { get; }
        IPrimaryItem CreatePrimary();
        ISecondaryItem CreateSecondary();
    }

    private interface IPrimaryItem
    {
        string Describe();
    }

    private interface ISecondaryItem
    {
        string Describe();
    }

    private sealed class InstitutionalTradingFactory : IBundleFactory
    {
        public string Name => "InstitutionalTradingFactory";

        public IPrimaryItem CreatePrimary() => new PrimaryItem();

        public ISecondaryItem CreateSecondary() => new SecondaryItem();
    }

    private sealed class PrimaryItem : IPrimaryItem
    {
        public string Describe() => "Created blotter: Institutional blotter showing block trades";
    }

    private sealed class SecondaryItem : ISecondaryItem
    {
        public string Describe() => "Created risk widget: Risk widget with VaR and stress metrics";
    }
}
