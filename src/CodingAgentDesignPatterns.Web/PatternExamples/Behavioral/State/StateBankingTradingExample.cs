namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.State;

public static class StateBankingTradingExample
{
    // The context delegates behavior to its current state object.
    // When the state changes, the next action can behave differently.
    public static IReadOnlyList<string> Run()
    {
        var context = new TradeOrder(new NewOrderState());
        var first = context.Handle();
        context.TransitionTo(new ApprovedOrderState());
        var second = context.Handle();

        return new[]
        {
            "Order lifecycle states",
            first,
            second
        };
    }

    private interface IState
    {
        string Handle();
    }

    private sealed class TradeOrder
    {
        private IState _state;

        public TradeOrder(IState state) => _state = state;

        public void TransitionTo(IState state) => _state = state;

        public string Handle() => _state.Handle();
    }

    private sealed class NewOrderState : IState
    {
        public string Handle() => "Moved to the next state.";
    }

    private sealed class ApprovedOrderState : IState
    {
        public string Handle() => "Order completed";
    }
}
