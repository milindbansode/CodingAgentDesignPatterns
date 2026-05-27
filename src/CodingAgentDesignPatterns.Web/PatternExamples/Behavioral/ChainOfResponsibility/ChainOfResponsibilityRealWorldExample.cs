namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.ChainOfResponsibility;

public static class ChainOfResponsibilityRealWorldExample
{
    // Every handler checks one condition.
    // If the handler cannot finish the request, it forwards the request to the next handler.
    public static IReadOnlyList<string> Run()
    {
        var first = new TeamLeadHandler();
        var second = new DirectorHandler();
        first.SetNext(second);

        return new[]
        {
            "Expense approval chain",
            $"Request: office chair purchase for 750",
            first.Handle(750)
        };
    }

    private abstract class Handler
    {
        private Handler? _next;

        public void SetNext(Handler next) => _next = next;

        public virtual string Handle(decimal amount) => _next?.Handle(amount) ?? "No handler accepted the request.";
    }

    private sealed class TeamLeadHandler : Handler
    {
        public override string Handle(decimal amount)
        {
            if (amount <= 500)
            {
                return "TeamLeadHandler approved the request.";
            }

            return base.Handle(amount);
        }
    }

    private sealed class DirectorHandler : Handler
    {
        public override string Handle(decimal amount)
        {
            return "Director approved 750";
        }
    }
}
