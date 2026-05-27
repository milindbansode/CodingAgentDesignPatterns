namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Command;

public static class CommandBankingTradingExample
{
    // The invoker holds a command object.
    // The command knows which receiver to call and what action to trigger.
    public static IReadOnlyList<string> Run()
    {
        var receiver = new TradeBlotter();
        ICommand command = new PlaceTradeCommand(receiver);
        var invoker = new Invoker(command);

        return new[]
        {
            "Trade blotter command",
            invoker.Run()
        };
    }

    private interface ICommand
    {
        string Execute();
    }

    private sealed class TradeBlotter
    {
        public string Activate() => "Trade submitted to the blotter";
    }

    private sealed class PlaceTradeCommand : ICommand
    {
        private readonly TradeBlotter _receiver;

        public PlaceTradeCommand(TradeBlotter receiver) => _receiver = receiver;

        public string Execute() => _receiver.Activate();
    }

    private sealed class Invoker
    {
        private readonly ICommand _command;

        public Invoker(ICommand command) => _command = command;

        public string Run() => _command.Execute();
    }
}
