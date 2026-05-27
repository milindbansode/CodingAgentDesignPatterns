namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.ChainOfResponsibility;

public static class ChainOfResponsibilityBankingTradingExample
{
    // Every handler checks one condition.
    // If the handler cannot finish the request, it forwards the request to the next handler.
    public static IReadOnlyList<string> Run()
    {
        var first = new LimitCheckHandler();
        var second = new ComplianceCheckHandler();
        first.SetNext(second);

        return new[]
        {
            "Trade validation chain",
            $"Request: equity order for 250000",
            first.Handle(250000)
        };
    }

    private abstract class Handler
    {
        private Handler? _next;

        public void SetNext(Handler next) => _next = next;

        public virtual string Handle(decimal amount) => _next?.Handle(amount) ?? "No handler accepted the request.";
    }

    private sealed class LimitCheckHandler : Handler
    {
        public override string Handle(decimal amount)
        {
            if (amount <= 500)
            {
                return "LimitCheckHandler approved the request.";
            }

            return base.Handle(amount);
        }
    }

    private sealed class ComplianceCheckHandler : Handler
    {
        public override string Handle(decimal amount)
        {
            return "Compliance approved the order";
        }
    }
}
