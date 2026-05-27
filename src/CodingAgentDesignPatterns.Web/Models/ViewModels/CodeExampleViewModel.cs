using CodingAgentDesignPatterns.Web.Models.Catalog;

namespace CodingAgentDesignPatterns.Web.Models.ViewModels;

public sealed record CodeExampleViewModel(
    PatternExampleDefinition Definition,
    string SourceCode,
    IReadOnlyList<string> SampleOutput);
