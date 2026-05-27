namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Observer;

public static class ObserverBankingTradingExample
{
    // Subscribers register once and then receive updates automatically.
    // The subject knows only the observer interface, not the concrete screens.
    public static IReadOnlyList<string> Run()
    {
        var subject = new PriceTicker();
        subject.Subscribe(new SalesDashboard());
        subject.Subscribe(new RiskMonitor());

        return subject.Publish("EUR/USD 1.0842");
    }

    private interface IObserver
    {
        string Update(string value);
    }

    private sealed class PriceTicker
    {
        private readonly List<IObserver> _observers = new();

        public void Subscribe(IObserver observer) => _observers.Add(observer);

        public IReadOnlyList<string> Publish(string value)
        {
            var messages = new List<string> { "Price ticker subscribers" };
            messages.AddRange(_observers.Select(observer => observer.Update(value)));
            return messages;
        }
    }

    private sealed class SalesDashboard : IObserver
    {
        public string Update(string value) => $"SalesDashboard received: {value}";
    }

    private sealed class RiskMonitor : IObserver
    {
        public string Update(string value) => $"RiskMonitor received: {value}";
    }
}
