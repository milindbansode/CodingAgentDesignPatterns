using System.Reflection;
using CodingAgentDesignPatterns.Web.Models.Catalog;

namespace CodingAgentDesignPatterns.Web.Services;

public sealed class DesignPatternCatalogService : IDesignPatternCatalogService
{
    private readonly string _contentRootPath;
    private readonly Assembly _assembly;
    private readonly IReadOnlyList<DesignPatternDefinition> _patterns;

    public DesignPatternCatalogService(IWebHostEnvironment environment)
    {
        _contentRootPath = environment.ContentRootPath;
        _assembly = typeof(Program).Assembly;
        _patterns = BuildPatterns();
    }

    public IReadOnlyList<DesignPatternDefinition> GetAllPatterns() => _patterns;

    public DesignPatternDefinition? GetPatternBySlug(string slug) =>
        _patterns.FirstOrDefault(pattern => string.Equals(pattern.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public string ReadSourceCode(string relativeSourcePath)
    {
        var fullPath = Path.GetFullPath(Path.Combine(_contentRootPath, relativeSourcePath));
        var normalizedRoot = Path.TrimEndingDirectorySeparator(_contentRootPath) + Path.DirectorySeparatorChar;
        if (!fullPath.StartsWith(normalizedRoot, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The requested source path is outside the application root.");
        }

        return File.Exists(fullPath)
            ? File.ReadAllText(fullPath)
            : "Source file not found.";
    }

    public IReadOnlyList<string> GetSampleOutput(string typeName)
    {
        var type = _assembly.GetType(typeName);
        var method = type?.GetMethod("Run", BindingFlags.Public | BindingFlags.Static);
        var result = method?.Invoke(null, null);

        if (result is IReadOnlyList<string> output)
        {
            return output;
        }

        if (result is IEnumerable<string> enumerable)
        {
            return enumerable.ToList();
        }

        return new[] { "Sample output is unavailable for this example." };
    }

    private static IReadOnlyList<DesignPatternDefinition> BuildPatterns()
    {
        return new List<DesignPatternDefinition>
        {
            new(
                "abstract-factory",
                "Abstract Factory",
                "Creational",
                "Create families of related objects without hard-coding the concrete classes.",
                "The client asks for a family and receives matching objects that already belong together.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Workspace kit selector",
                        "Real world",
                        "A workspace factory creates a matching desk and chair for a chosen room setup.",
                        "PatternExamples/Creational/AbstractFactory/AbstractFactoryRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.AbstractFactory.AbstractFactoryRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Trading desk dashboard kit",
                        "Banking / trading",
                        "A trading dashboard factory creates related blotter and risk widgets for one desk type.",
                        "PatternExamples/Creational/AbstractFactory/AbstractFactoryBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.AbstractFactory.AbstractFactoryBankingTradingExample"),
                }),
            new(
                "builder",
                "Builder",
                "Creational",
                "Build a complex object step by step while keeping construction readable.",
                "The same process can create different final shapes without a giant constructor.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Weekend trip planner",
                        "Real world",
                        "A trip builder assembles a simple travel plan in clear construction steps.",
                        "PatternExamples/Creational/Builder/BuilderRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.Builder.BuilderRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Trade ticket builder",
                        "Banking / trading",
                        "A trade ticket builder assembles an order ticket with validation-friendly steps.",
                        "PatternExamples/Creational/Builder/BuilderBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.Builder.BuilderBankingTradingExample"),
                }),
            new(
                "factory-method",
                "Factory Method",
                "Creational",
                "Let subclasses decide which concrete object to create.",
                "The caller talks to a shared abstraction while concrete creation moves into specialized creators.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Notification sender",
                        "Real world",
                        "A sender chooses the correct delivery channel by overriding one factory method.",
                        "PatternExamples/Creational/FactoryMethod/FactoryMethodRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.FactoryMethod.FactoryMethodRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Order processor selector",
                        "Banking / trading",
                        "A trading workflow picks the right processor for a market-specific order.",
                        "PatternExamples/Creational/FactoryMethod/FactoryMethodBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.FactoryMethod.FactoryMethodBankingTradingExample"),
                }),
            new(
                "prototype",
                "Prototype",
                "Creational",
                "Clone an existing object instead of rebuilding it from scratch.",
                "A prepared template can be copied quickly and then lightly customized.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Visitor badge template",
                        "Real world",
                        "A preconfigured badge is cloned and personalized for a new visitor.",
                        "PatternExamples/Creational/Prototype/PrototypeRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.Prototype.PrototypeRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Client quote template",
                        "Banking / trading",
                        "A saved quote template is cloned before tailoring it to a specific client request.",
                        "PatternExamples/Creational/Prototype/PrototypeBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.Prototype.PrototypeBankingTradingExample"),
                }),
            new(
                "singleton",
                "Singleton",
                "Creational",
                "Guarantee a single shared instance for a service or resource.",
                "Every caller reads the same state from one well-known access point.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "App settings store",
                        "Real world",
                        "A single settings store keeps shared configuration values for the whole app.",
                        "PatternExamples/Creational/Singleton/SingletonRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.Singleton.SingletonRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Market hours calendar",
                        "Banking / trading",
                        "A single market calendar keeps open and close information consistent.",
                        "PatternExamples/Creational/Singleton/SingletonBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Creational.Singleton.SingletonBankingTradingExample"),
                }),
            new(
                "adapter",
                "Adapter",
                "Structural",
                "Convert one interface into another that the client already understands.",
                "The client keeps its preferred contract while the adapter wraps the incompatible service.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Smart plug adapter",
                        "Real world",
                        "A wall socket expects one shape, so an adapter lets a USB device fit in.",
                        "PatternExamples/Structural/Adapter/AdapterRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Adapter.AdapterRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Legacy price feed adapter",
                        "Banking / trading",
                        "A new pricing screen consumes a clean interface while the adapter wraps a legacy feed.",
                        "PatternExamples/Structural/Adapter/AdapterBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Adapter.AdapterBankingTradingExample"),
                }),
            new(
                "bridge",
                "Bridge",
                "Structural",
                "Split an abstraction from its implementation so both can evolve independently.",
                "You can mix one high-level abstraction with many implementations without subclass explosion.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Remote control and device",
                        "Real world",
                        "The remote abstraction works with any device implementation.",
                        "PatternExamples/Structural/Bridge/BridgeRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Bridge.BridgeRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Risk report and delivery channel",
                        "Banking / trading",
                        "One report type can be delivered through different communication channels.",
                        "PatternExamples/Structural/Bridge/BridgeBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Bridge.BridgeBankingTradingExample"),
                }),
            new(
                "composite",
                "Composite",
                "Structural",
                "Treat single objects and groups of objects through the same interface.",
                "Clients can work with a tree structure without caring whether they have a leaf or a container.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Folder tree",
                        "Real world",
                        "Files and folders share one interface so totals can be calculated recursively.",
                        "PatternExamples/Structural/Composite/CompositeRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Composite.CompositeRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Portfolio hierarchy",
                        "Banking / trading",
                        "Books and positions share one interface so exposure rolls up naturally.",
                        "PatternExamples/Structural/Composite/CompositeBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Composite.CompositeBankingTradingExample"),
                }),
            new(
                "decorator",
                "Decorator",
                "Structural",
                "Add responsibilities to an object dynamically by wrapping it.",
                "New behavior is layered on without changing the original class.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Coffee customizer",
                        "Real world",
                        "A simple coffee gains toppings by being wrapped with decorators.",
                        "PatternExamples/Structural/Decorator/DecoratorRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Decorator.DecoratorRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Order ticket enrichment",
                        "Banking / trading",
                        "An order ticket is wrapped with extra compliance and fee details.",
                        "PatternExamples/Structural/Decorator/DecoratorBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Decorator.DecoratorBankingTradingExample"),
                }),
            new(
                "facade",
                "Facade",
                "Structural",
                "Provide one simple entry point over a more complex subsystem.",
                "Clients get a readable workflow without knowing every moving part underneath.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Trip booking facade",
                        "Real world",
                        "One booking call hides the steps for flights, hotel, and transport.",
                        "PatternExamples/Structural/Facade/FacadeRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Facade.FacadeRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Trade execution facade",
                        "Banking / trading",
                        "One execution call hides checks, booking, and confirmation steps.",
                        "PatternExamples/Structural/Facade/FacadeBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Facade.FacadeBankingTradingExample"),
                }),
            new(
                "flyweight",
                "Flyweight",
                "Structural",
                "Share reusable intrinsic state so many objects stay lightweight.",
                "Large collections consume less memory when repeated data is stored once.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Park tree map",
                        "Real world",
                        "Many trees share the same tree type details instead of duplicating them.",
                        "PatternExamples/Structural/Flyweight/FlyweightRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Flyweight.FlyweightRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Instrument definition cache",
                        "Banking / trading",
                        "Many positions share a single instrument definition for the same ticker.",
                        "PatternExamples/Structural/Flyweight/FlyweightBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Flyweight.FlyweightBankingTradingExample"),
                }),
            new(
                "proxy",
                "Proxy",
                "Structural",
                "Place a stand-in object in front of a real object to control access.",
                "The proxy can delay work, check permissions, or cache results before touching the real object.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Image loader proxy",
                        "Real world",
                        "A proxy delays loading a large image until the screen really needs it.",
                        "PatternExamples/Structural/Proxy/ProxyRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Proxy.ProxyRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Risk report proxy",
                        "Banking / trading",
                        "A proxy checks permissions and reuses the latest risk report when possible.",
                        "PatternExamples/Structural/Proxy/ProxyBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Structural.Proxy.ProxyBankingTradingExample"),
                }),
            new(
                "chain-of-responsibility",
                "Chain of Responsibility",
                "Behavioral",
                "Pass a request through a chain until one handler deals with it.",
                "Each handler focuses on one rule and forwards the request when it cannot finish the job.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Expense approval chain",
                        "Real world",
                        "Each manager approves expenses up to a limit and passes larger ones onward.",
                        "PatternExamples/Behavioral/ChainOfResponsibility/ChainOfResponsibilityRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.ChainOfResponsibility.ChainOfResponsibilityRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Trade validation chain",
                        "Banking / trading",
                        "A trade moves through limit, compliance, and market-status checks.",
                        "PatternExamples/Behavioral/ChainOfResponsibility/ChainOfResponsibilityBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.ChainOfResponsibility.ChainOfResponsibilityBankingTradingExample"),
                }),
            new(
                "command",
                "Command",
                "Behavioral",
                "Wrap a request as an object so it can be queued, logged, or undone.",
                "The caller triggers commands without knowing the details of the receiver.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Lamp remote command",
                        "Real world",
                        "A remote stores a command object that knows how to turn on the lamp.",
                        "PatternExamples/Behavioral/Command/CommandRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Command.CommandRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Trade blotter command",
                        "Banking / trading",
                        "A blotter button triggers a command that knows how to place the trade.",
                        "PatternExamples/Behavioral/Command/CommandBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Command.CommandBankingTradingExample"),
                }),
            new(
                "interpreter",
                "Interpreter",
                "Behavioral",
                "Represent a small language with classes so expressions can be evaluated.",
                "Grammar rules become objects that can be combined to read a simple domain-specific language.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Packing checklist rule",
                        "Real world",
                        "A tiny rule language checks whether a packing list is ready.",
                        "PatternExamples/Behavioral/Interpreter/InterpreterRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Interpreter.InterpreterRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Client eligibility rule",
                        "Banking / trading",
                        "A rule expression checks whether a client matches a simple trading policy.",
                        "PatternExamples/Behavioral/Interpreter/InterpreterBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Interpreter.InterpreterBankingTradingExample"),
                }),
            new(
                "iterator",
                "Iterator",
                "Behavioral",
                "Traverse a collection without exposing its internal representation.",
                "The collection controls traversal details while callers use a simple iteration contract.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Playlist iterator",
                        "Real world",
                        "A custom iterator walks through songs in order without exposing the backing list.",
                        "PatternExamples/Behavioral/Iterator/IteratorRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Iterator.IteratorRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Watchlist iterator",
                        "Banking / trading",
                        "A trading watchlist exposes a cursor-like iterator for symbols.",
                        "PatternExamples/Behavioral/Iterator/IteratorBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Iterator.IteratorBankingTradingExample"),
                }),
            new(
                "mediator",
                "Mediator",
                "Behavioral",
                "Centralize communication between collaborators in one mediator object.",
                "Colleagues stop talking to each other directly, which keeps relationships simpler.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Family chat room",
                        "Real world",
                        "Family members send messages through a chat room mediator.",
                        "PatternExamples/Behavioral/Mediator/MediatorRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Mediator.MediatorRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Trade desk coordinator",
                        "Banking / trading",
                        "Sales, traders, and risk teams coordinate through one desk mediator.",
                        "PatternExamples/Behavioral/Mediator/MediatorBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Mediator.MediatorBankingTradingExample"),
                }),
            new(
                "memento",
                "Memento",
                "Behavioral",
                "Capture and restore an object state without exposing its internals.",
                "Undo-like behavior becomes possible while the originator keeps control of its own data.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Document draft undo",
                        "Real world",
                        "A draft saves checkpoints so the last safe version can be restored.",
                        "PatternExamples/Behavioral/Memento/MementoRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Memento.MementoRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Order ticket undo",
                        "Banking / trading",
                        "An order ticket stores snapshots before a user makes more edits.",
                        "PatternExamples/Behavioral/Memento/MementoBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Memento.MementoBankingTradingExample"),
                }),
            new(
                "observer",
                "Observer",
                "Behavioral",
                "Notify many subscribers automatically when a subject changes.",
                "Publishers and subscribers stay loosely coupled while updates flow in real time.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Weather station updates",
                        "Real world",
                        "Displays subscribe to the weather station and react whenever the forecast changes.",
                        "PatternExamples/Behavioral/Observer/ObserverRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Observer.ObserverRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Price ticker subscribers",
                        "Banking / trading",
                        "Trading screens subscribe to a ticker so price updates fan out automatically.",
                        "PatternExamples/Behavioral/Observer/ObserverBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Observer.ObserverBankingTradingExample"),
                }),
            new(
                "state",
                "State",
                "Behavioral",
                "Change behavior when an object moves from one state to another.",
                "State-specific rules live in separate classes instead of one long if/else block.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Vending machine states",
                        "Real world",
                        "The machine reacts differently before and after a payment is inserted.",
                        "PatternExamples/Behavioral/State/StateRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.State.StateRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Order lifecycle states",
                        "Banking / trading",
                        "An order behaves differently while new, approved, or completed.",
                        "PatternExamples/Behavioral/State/StateBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.State.StateBankingTradingExample"),
                }),
            new(
                "strategy",
                "Strategy",
                "Behavioral",
                "Swap algorithms at runtime by selecting a strategy object.",
                "The context stays small while the algorithm varies independently.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Route planning strategy",
                        "Real world",
                        "A route planner chooses a walking or driving algorithm without changing the context.",
                        "PatternExamples/Behavioral/Strategy/StrategyRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Strategy.StrategyRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Fee calculation strategy",
                        "Banking / trading",
                        "A ticket picks the fee algorithm that fits the client segment.",
                        "PatternExamples/Behavioral/Strategy/StrategyBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Strategy.StrategyBankingTradingExample"),
                }),
            new(
                "template-method",
                "Template Method",
                "Behavioral",
                "Define the skeleton of an algorithm and let subclasses fill in the changing steps.",
                "Shared workflow stays in one place while special steps are customized in derived classes.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Hot drink recipe",
                        "Real world",
                        "A drink recipe keeps the same order while subclasses choose the ingredients.",
                        "PatternExamples/Behavioral/TemplateMethod/TemplateMethodRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.TemplateMethod.TemplateMethodRealWorldExample"),
                    new(
                        "bankingtrading",
                        "End-of-day report workflow",
                        "Banking / trading",
                        "A reporting workflow keeps common steps while concrete reports fill in the details.",
                        "PatternExamples/Behavioral/TemplateMethod/TemplateMethodBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.TemplateMethod.TemplateMethodBankingTradingExample"),
                }),
            new(
                "visitor",
                "Visitor",
                "Behavioral",
                "Add new operations to an object structure without changing the element classes.",
                "Elements stay stable while visitors carry each new operation across the structure.",
                new List<PatternExampleDefinition>
                {
                    new(
                        "realworld",
                        "Shopping cart visitor",
                        "Real world",
                        "Different visitors calculate totals without modifying the item classes.",
                        "PatternExamples/Behavioral/Visitor/VisitorRealWorldExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Visitor.VisitorRealWorldExample"),
                    new(
                        "bankingtrading",
                        "Portfolio analytics visitor",
                        "Banking / trading",
                        "Different analytics can visit the same positions without changing the position types.",
                        "PatternExamples/Behavioral/Visitor/VisitorBankingTradingExample.cs",
                        "CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.Visitor.VisitorBankingTradingExample"),
                }),
        };
    }
}
