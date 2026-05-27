namespace CodingAgentDesignPatterns.Web.PatternExamples.Structural.Composite;

public static class CompositeBankingTradingExample
{
    // Leaf objects and container objects both implement the same interface.
    // That lets the client ask the whole tree for one combined value.
    public static IReadOnlyList<string> Run()
    {
        var root = new Group("Global Macro");
        root.Add(new Leaf("Rates position", 15));
        root.Add(new Leaf("FX position", 9));

        return new[]
        {
            "Portfolio hierarchy",
            $"Root: {root.Name}",
            $"Total mm USD: {root.GetTotal()}"
        };
    }

    private interface INode
    {
        int GetTotal();
    }

    private sealed class Leaf : INode
    {
        public Leaf(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public int Value { get; }

        public int GetTotal() => Value;
    }

    private sealed class Group : INode
    {
        private readonly List<INode> _children = new();

        public Group(string name) => Name = name;

        public string Name { get; }

        public void Add(INode node) => _children.Add(node);

        public int GetTotal() => _children.Sum(child => child.GetTotal());
    }
}
