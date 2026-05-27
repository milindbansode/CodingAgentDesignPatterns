using CodingAgentDesignPatterns.Web.Models.Catalog;

namespace CodingAgentDesignPatterns.Web.Models.ViewModels;

public sealed record PatternCatalogViewModel(
    IReadOnlyList<DesignPatternDefinition> Patterns,
    IReadOnlyList<CategorySummaryViewModel> Categories);
