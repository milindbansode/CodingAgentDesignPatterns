namespace CodingAgentDesignPatterns.Web.Models.Catalog;

public sealed record DesignPatternDefinition(
    string Slug,
    string Name,
    string Category,
    string Intent,
    string WhyItHelps,
    IReadOnlyList<PatternExampleDefinition> Examples);
