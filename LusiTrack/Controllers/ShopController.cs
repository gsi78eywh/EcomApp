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

    public IActionResult Index(string? category)
    {
        var selectedCategory = string.IsNullOrWhiteSpace(category) ? "All" : category;
        var products = _catalogService.GetProductsByCategory(selectedCategory);

        ViewBag.Categories = _catalogService.GetCategories();
        ViewBag.SelectedCategory = selectedCategory;

        return View(products);
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
