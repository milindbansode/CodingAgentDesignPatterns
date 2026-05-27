namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Command;

public static class CommandRealWorldExample
{
    // The invoker holds a command object.
    // The command knows which receiver to call and what action to trigger.
    public static IReadOnlyList<string> Run()
    {
        var receiver = new Lamp();
        ICommand command = new TurnOnCommand(receiver);
        var invoker = new Invoker(command);

        return new[]
        {
            "Lamp remote command",
            invoker.Run()
        };
    }

    private interface ICommand
    {
        string Execute();
    }

    private sealed class Lamp
    {
        public string Activate() => "Lamp turned on";
    }

    private sealed class TurnOnCommand : ICommand
    {
        private readonly Lamp _receiver;

        public TurnOnCommand(Lamp receiver) => _receiver = receiver;

        public string Execute() => _receiver.Activate();
    }

    private sealed class Invoker
    {
        private readonly ICommand _command;

        public Invoker(ICommand command) => _command = command;

        public string Run() => _command.Execute();
    }
}
