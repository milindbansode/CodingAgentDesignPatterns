using CodingAgentDesignPatterns.Web.Models.Catalog;

namespace CodingAgentDesignPatterns.Web.Models.ViewModels;

public sealed record PatternDetailsViewModel(
    DesignPatternDefinition Pattern,
    IReadOnlyList<CodeExampleViewModel> Examples);
