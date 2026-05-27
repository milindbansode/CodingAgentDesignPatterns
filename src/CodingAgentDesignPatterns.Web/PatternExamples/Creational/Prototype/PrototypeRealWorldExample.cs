namespace CodingAgentDesignPatterns.Web.PatternExamples.Creational.Prototype;

public static class PrototypeRealWorldExample
{
    // The prototype already holds the expensive setup.
    // Cloning copies that work so the new object only changes the fields it needs.
    public static IReadOnlyList<string> Run()
    {
        var template = new ConfigurableItem("visitor badge", "Blue");
        var clone = template.Clone();
        clone.Value = "Green";

        return new[]
        {
            "Visitor badge template",
            $"Template BadgeColor: {template.Value}",
            $"Cloned BadgeColor: {clone.Value}"
        };
    }

    private sealed class ConfigurableItem
    {
        public ConfigurableItem(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; set; }

        public ConfigurableItem Clone()
        {
            // A shallow copy is enough because this tiny example stores simple values only.
            return (ConfigurableItem)MemberwiseClone();
        }
    }
}
