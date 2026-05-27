namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Mediator;

public static class MediatorBankingTradingExample
{
    // Colleagues talk to the mediator, not to each other.
    // The mediator decides how to route the message.
    public static IReadOnlyList<string> Run()
    {
        var mediator = new TradeDeskMediator();
        var first = new Participant("Sales", mediator);
        var second = new Participant("Risk", mediator);
        mediator.Register(first);
        mediator.Register(second);

        return new[]
        {
            "Trade desk coordinator",
            first.Send("Status update ready"),
            second.Send("Acknowledged")
        };
    }

    private sealed class TradeDeskMediator
    {
        private readonly List<Participant> _participants = new();

        public void Register(Participant participant) => _participants.Add(participant);

        public string Broadcast(string from, string message)
        {
            var receivers = _participants.Where(participant => participant.Name != from).Select(participant => participant.Name);
            return $"{from} -> {string.Join(", ", receivers)}: {message}";
        }
    }

    private sealed class Participant
    {
        private readonly TradeDeskMediator _mediator;

        public Participant(string name, TradeDeskMediator mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public string Name { get; }

        public string Send(string message) => _mediator.Broadcast(Name, message);
    }
}
