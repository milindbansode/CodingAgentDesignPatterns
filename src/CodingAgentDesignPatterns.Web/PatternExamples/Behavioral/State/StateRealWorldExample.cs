namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.State;

public static class StateRealWorldExample
{
    // The context delegates behavior to its current state object.
    // When the state changes, the next action can behave differently.
    public static IReadOnlyList<string> Run()
    {
        var context = new VendingMachine(new WaitingForPaymentState());
        var first = context.Handle();
        context.TransitionTo(new DispensingState());
        var second = context.Handle();

        return new[]
        {
            "Vending machine states",
            first,
            second
        };
    }

    private interface IState
    {
        string Handle();
    }

    private sealed class VendingMachine
    {
        private IState _state;

        public VendingMachine(IState state) => _state = state;

        public void TransitionTo(IState state) => _state = state;

        public string Handle() => _state.Handle();
    }

    private sealed class WaitingForPaymentState : IState
    {
        public string Handle() => "Moved to the next state.";
    }

    private sealed class DispensingState : IState
    {
        public string Handle() => "Snack dispensed";
    }
}
