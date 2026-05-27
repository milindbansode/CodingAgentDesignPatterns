namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Visitor;

public static class VisitorRealWorldExample
{
    // Elements stay focused on their own data.
    // The visitor brings the operation that should run across every element.
    public static IReadOnlyList<string> Run()
    {
        IElement[] elements = { new Book(), new Headphones() };
        var visitor = new ReceiptVisitor();
        var messages = new List<string> { "Shopping cart visitor" };
        messages.AddRange(elements.Select(element => element.Accept(visitor)));
        messages.Add("Receipt built for 2 items");
        return messages;
    }

    private interface IElement
    {
        string Accept(IVisitor visitor);
    }

    private interface IVisitor
    {
        string Visit(Book element);
        string Visit(Headphones element);
    }

    private sealed class Book : IElement
    {
        public string Accept(IVisitor visitor) => visitor.Visit(this);
    }

    private sealed class Headphones : IElement
    {
        public string Accept(IVisitor visitor) => visitor.Visit(this);
    }

    private sealed class ReceiptVisitor : IVisitor
    {
        public string Visit(Book element) => "Visited Book.";
        public string Visit(Headphones element) => "Visited Headphones.";
    }
}
