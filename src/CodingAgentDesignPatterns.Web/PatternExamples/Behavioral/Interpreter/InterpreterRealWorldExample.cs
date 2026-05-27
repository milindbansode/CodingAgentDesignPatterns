namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Interpreter;

public static class InterpreterRealWorldExample
{
    // The context stores the words that are available.
    // Expression objects read the context and combine smaller rules into a bigger rule.
    public static IReadOnlyList<string> Run()
    {
        var context = new Context(new[] { "Passport", "Phone Charger", "Ready" });
        IExpression expression = new AndExpression(new TerminalExpression("Passport"), new TerminalExpression("Phone Charger"));
        var isReady = expression.Interpret(context);

        return new[]
        {
            "Packing checklist rule",
            $"Checklist ready: {isReady}"
        };
    }

    private sealed class Context
    {
        public Context(IEnumerable<string> words) => Words = new HashSet<string>(words);

        public HashSet<string> Words { get; }
    }

    private interface IExpression
    {
        bool Interpret(Context context);
    }

    private sealed class TerminalExpression : IExpression
    {
        private readonly string _word;

        public TerminalExpression(string word) => _word = word;

        public bool Interpret(Context context) => context.Words.Contains(_word);
    }

    private sealed class AndExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public AndExpression(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public bool Interpret(Context context) => _left.Interpret(context) && _right.Interpret(context);
    }
}
