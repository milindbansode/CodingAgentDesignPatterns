namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Visitor;

public static class VisitorBankingTradingExample
{
    // Elements stay focused on their own data.
    // The visitor brings the operation that should run across every element.
    public static IReadOnlyList<string> Run()
    {
        IElement[] elements = { new BondPosition(), new EquityPosition() };
        var visitor = new ExposureVisitor();
        var messages = new List<string> { "Portfolio analytics visitor" };
        messages.AddRange(elements.Select(element => element.Accept(visitor)));
        messages.Add("Exposure calculated for bond and equity");
        return messages;
    }

    private interface IElement
    {
        string Accept(IVisitor visitor);
    }

    private interface IVisitor
    {
        string Visit(BondPosition element);
        string Visit(EquityPosition element);
    }

    private sealed class BondPosition : IElement
    {
        public string Accept(IVisitor visitor) => visitor.Visit(this);
    }

    private sealed class EquityPosition : IElement
    {
        public string Accept(IVisitor visitor) => visitor.Visit(this);
    }

    private sealed class ExposureVisitor : IVisitor
    {
        public string Visit(BondPosition element) => "Visited BondPosition.";
        public string Visit(EquityPosition element) => "Visited EquityPosition.";
    }
}
