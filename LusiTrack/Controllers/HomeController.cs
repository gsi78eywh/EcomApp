using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class HomeController : Controller
{
    private readonly ICoffeeCatalogService _catalogService;

    public HomeController(ICoffeeCatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public IActionResult Index()
    {
        var featuredProducts = _catalogService.GetFeaturedProducts();
        ViewBag.Categories = _catalogService.GetCategories();
        return View(featuredProducts);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
