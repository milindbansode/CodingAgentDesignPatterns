namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.AbstractFactory;

public static class AbstractFactoryRealWorldExample
{
    // This example keeps the client code simple.
    // The client chooses one factory and receives a matching pair of related objects.
    public static IReadOnlyList<string> Run()
    {
        IBundleFactory factory = new HomeOfficeFactory();
        var primary = factory.CreatePrimary();
        var secondary = factory.CreateSecondary();

        return new[]
        {
            "Workspace kit selector",
            $"Selected workspace kit: {factory.Name}",
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

    private sealed class HomeOfficeFactory : IBundleFactory
    {
        public string Name => "HomeOfficeFactory";

        public IPrimaryItem CreatePrimary() => new PrimaryItem();

        public ISecondaryItem CreateSecondary() => new SecondaryItem();
    }

    private sealed class PrimaryItem : IPrimaryItem
    {
        public string Describe() => "Created desk: Standing desk with a quiet motor";
    }

    private sealed class SecondaryItem : ISecondaryItem
    {
        public string Describe() => "Created chair: Ergonomic chair with lumbar support";
    }
}
