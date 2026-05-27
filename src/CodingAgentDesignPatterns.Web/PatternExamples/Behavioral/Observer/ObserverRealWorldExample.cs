namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Observer;

public static class ObserverRealWorldExample
{
    // Subscribers register once and then receive updates automatically.
    // The subject knows only the observer interface, not the concrete screens.
    public static IReadOnlyList<string> Run()
    {
        var subject = new WeatherStation();
        subject.Subscribe(new PhoneDisplay());
        subject.Subscribe(new KitchenDisplay());

        return subject.Publish("Rain this afternoon");
    }

    private interface IObserver
    {
        string Update(string value);
    }

    private sealed class WeatherStation
    {
        private readonly List<IObserver> _observers = new();

        public void Subscribe(IObserver observer) => _observers.Add(observer);

        public IReadOnlyList<string> Publish(string value)
        {
            var messages = new List<string> { "Weather station updates" };
            messages.AddRange(_observers.Select(observer => observer.Update(value)));
            return messages;
        }
    }

    private sealed class PhoneDisplay : IObserver
    {
        public string Update(string value) => $"PhoneDisplay received: {value}";
    }

    private sealed class KitchenDisplay : IObserver
    {
        public string Update(string value) => $"KitchenDisplay received: {value}";
    }
}
