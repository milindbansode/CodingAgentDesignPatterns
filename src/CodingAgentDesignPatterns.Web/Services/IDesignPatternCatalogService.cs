using CodingAgentDesignPatterns.Web.Models.Catalog;

namespace CodingAgentDesignPatterns.Web.Services;

public interface IDesignPatternCatalogService
{
    IReadOnlyList<DesignPatternDefinition> GetAllPatterns();

    DesignPatternDefinition? GetPatternBySlug(string slug);

    string ReadSourceCode(string relativeSourcePath);

    IReadOnlyList<string> GetSampleOutput(string typeName);
}
