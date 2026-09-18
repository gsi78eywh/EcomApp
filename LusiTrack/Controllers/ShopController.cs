using Microsoft.AspNetCore.Mvc;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class ShopController : Controller
{
    private readonly ICoffeeCatalogService _catalogService;

    public ShopController(ICoffeeCatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public IActionResult Index(string? category, string? search, int page = 1, int pageSize = 6)
    {
        var selectedCategory = string.IsNullOrWhiteSpace(category) ? "All" : category.Trim();
        var searchQuery = string.IsNullOrWhiteSpace(search) ? null : search.Trim();

        var allMatching = _catalogService.SearchProducts(searchQuery, selectedCategory).ToList();

        var totalItems = allMatching.Count;
        page = Math.Max(1, page);
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        if (totalPages > 0 && page > totalPages) page = totalPages;

        var pagedProducts = allMatching.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        // Compute item counts per category for the filter badges based on current search query
        var allSearched = _catalogService.SearchProducts(searchQuery, "All").ToList();
        var categoryCounts = _catalogService.GetCategories()
            .ToDictionary(c => c, c => allSearched.Count(p => string.Equals(p.Category, c, StringComparison.OrdinalIgnoreCase)));
        categoryCounts["All"] = allSearched.Count;

        ViewBag.Categories = _catalogService.GetCategories();
        ViewBag.SelectedCategory = selectedCategory;
        ViewBag.SearchQuery = searchQuery;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = Math.Max(1, totalPages);
        ViewBag.TotalItems = totalItems;
        ViewBag.PageSize = pageSize;
        ViewBag.CategoryCounts = categoryCounts;

        return View(pagedProducts);
    }

    public IActionResult Details(int id)
    {
        var product = _catalogService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        var relatedProducts = _catalogService.GetProductsByCategory(product.Category)
            .Where(p => p.Id != id)
            .Take(3);

        ViewBag.Related = relatedProducts;
        return View(product);
    }
}
