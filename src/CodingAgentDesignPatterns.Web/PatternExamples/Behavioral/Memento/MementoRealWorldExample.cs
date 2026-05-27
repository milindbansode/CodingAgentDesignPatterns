namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Memento;

public static class MementoRealWorldExample
{
    // The originator creates snapshots of its own state.
    // The caretaker stores those snapshots without needing to inspect their contents.
    public static IReadOnlyList<string> Run()
    {
        var originator = new DraftDocument();
        originator.Text = "First draft";

        var history = new Stack<DraftDocument.Snapshot>();
        history.Push(originator.Save());

        originator.Text = "First draft with edits";
        originator.Restore(history.Pop());

        return new[]
        {
            "Document draft undo",
            $"Restored text: {originator.Text}"
        };
    }

    private sealed class DraftDocument
    {
        public string Text { get; set; } = string.Empty;

        public Snapshot Save() => new(Text);

        public void Restore(Snapshot snapshot) => Text = snapshot.Text;

        public sealed record Snapshot(string Text);
    }
}
