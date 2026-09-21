using Microsoft.AspNetCore.Mvc;
using LusiTrack.Services;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

[Route("menu")]
[Route("Shop")]
public class ShopController : Controller
{
    private readonly ICoffeeCatalogService _catalogService;

    public ShopController(ICoffeeCatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public IActionResult Index(string? search, string? category, string? roast, decimal? minPrice, decimal? maxPrice, string? sortBy)
    {
        var selectedCategory = string.IsNullOrWhiteSpace(category) ? "All" : category;
        var products = _catalogService.SearchAndFilter(search, selectedCategory, roast, minPrice, maxPrice, sortBy);
        var allProducts = _catalogService.GetAllProducts().ToList();
        var categoryCounts = allProducts
            .GroupBy(p => p.Category)
            .ToDictionary(g => g.Key, g => g.Count(), StringComparer.OrdinalIgnoreCase);

        ViewBag.Categories = _catalogService.GetCategories();
        ViewBag.SelectedCategory = selectedCategory;
        ViewBag.SearchQuery = search ?? string.Empty;
        ViewBag.SelectedRoast = roast ?? "All";
        ViewBag.SortBy = sortBy ?? "recommended";
        ViewBag.TotalCount = products.Count();
        ViewBag.CategoryCounts = categoryCounts;
        ViewBag.CategoryDefinitions = JmtCafeConfig.CategoryDefinitions;
        ViewBag.AllProductsCount = allProducts.Count;

        return View(products);
    }

    [HttpGet("{id:int}")]
    [HttpGet("Details/{id:int}")]
    public IActionResult Details(int id)
    {
        var product = _catalogService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        var relatedProducts = _catalogService.GetProductsByCategory(product.Category)
            .Where(p => p.Id != id)
            .Take(3)
            .ToList();

        ViewBag.Related = relatedProducts;
        return View(product);
    }
}

