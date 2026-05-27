namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Mediator;

public static class MediatorRealWorldExample
{
    // Colleagues talk to the mediator, not to each other.
    // The mediator decides how to route the message.
    public static IReadOnlyList<string> Run()
    {
        var mediator = new FamilyChatRoom();
        var first = new Participant("Ava", mediator);
        var second = new Participant("Noah", mediator);
        mediator.Register(first);
        mediator.Register(second);

        return new[]
        {
            "Family chat room",
            first.Send("Status update ready"),
            second.Send("Acknowledged")
        };
    }

    private sealed class FamilyChatRoom
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
        private readonly FamilyChatRoom _mediator;

        public Participant(string name, FamilyChatRoom mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public string Name { get; }

        public string Send(string message) => _mediator.Broadcast(Name, message);
    }
}
