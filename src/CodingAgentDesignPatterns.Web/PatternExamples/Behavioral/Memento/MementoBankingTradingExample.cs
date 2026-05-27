namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Memento;

public static class MementoBankingTradingExample
{
    // The originator creates snapshots of its own state.
    // The caretaker stores those snapshots without needing to inspect their contents.
    public static IReadOnlyList<string> Run()
    {
        var originator = new OrderTicket();
        originator.Text = "Buy 100 AAPL";

        var history = new Stack<OrderTicket.Snapshot>();
        history.Push(originator.Save());

        originator.Text = "Buy 200 AAPL";
        originator.Restore(history.Pop());

        return new[]
        {
            "Order ticket undo",
            $"Restored text: {originator.Text}"
        };
    }

    private sealed class OrderTicket
    {
        public string Text { get; set; } = string.Empty;

        public Snapshot Save() => new(Text);

        public void Restore(Snapshot snapshot) => Text = snapshot.Text;

        public sealed record Snapshot(string Text);
    }
}
