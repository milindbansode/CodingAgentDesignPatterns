using CodingAgentDesignPatterns.Web.Models.ViewModels;
using CodingAgentDesignPatterns.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingAgentDesignPatterns.Web.Controllers;

public class PatternsController : Controller
{
    private readonly IDesignPatternCatalogService _catalogService;

    public PatternsController(IDesignPatternCatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("patterns")]
    public IActionResult Index()
    {
        var patterns = _catalogService.GetAllPatterns();
        var categories = patterns
            .GroupBy(pattern => pattern.Category)
            .OrderBy(group => group.Key)
            .Select(group => new CategorySummaryViewModel(group.Key, group.Count()))
            .ToList();

        return View(new PatternCatalogViewModel(patterns, categories));
    }

    [HttpGet("patterns/{slug}")]
    public IActionResult Details(string slug)
    {
        var pattern = _catalogService.GetPatternBySlug(slug);
        if (pattern is null)
        {
            return NotFound();
        }

        var examples = pattern.Examples
            .Select(example => new CodeExampleViewModel(
                example,
                _catalogService.ReadSourceCode(example.RelativeSourcePath),
                _catalogService.GetSampleOutput(example.TypeName)))
            .ToList();

        return View(new PatternDetailsViewModel(pattern, examples));
    }
}
