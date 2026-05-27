namespace CodingAgentDesignPatterns.Web.Models.Catalog;

public sealed record PatternExampleDefinition(
    string Id,
    string Title,
    string Scenario,
    string Summary,
    string RelativeSourcePath,
    string TypeName);
