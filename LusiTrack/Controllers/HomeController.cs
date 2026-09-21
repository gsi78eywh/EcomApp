using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using LusiTrack.Models;
using LusiTrack.Services;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class HomeController : Controller
{
    private readonly ICoffeeCatalogService _catalogService;

    public HomeController(ICoffeeCatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("")]
    [HttpGet("Home")]
    [HttpGet("Home/Index")]
    public IActionResult Index()
    {
        var bestSellers = _catalogService.GetBestSellers(8).ToList();
        ViewBag.BestSellers = bestSellers;
        ViewBag.FeaturedProducts = _catalogService.GetFeaturedProducts().ToList();
        ViewBag.Categories = _catalogService.GetCategories();

        return View(bestSellers);
    }

    [HttpGet("about")]
    [HttpGet("Home/About")]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet("contact")]
    [HttpGet("Home/Contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost("contact")]
    [HttpPost("Home/Contact")]
    [ValidateAntiForgeryToken]
    public IActionResult Contact(string name, string email, string category, string? orderNumber, string subject, string message)
    {
        TempData["SuccessMessage"] = $"Thank you, {name}! Your message regarding '{subject}' has been routed to our JMT Cafe Purok 5 Tabon staff.";
        return RedirectToAction("Contact");
    }

    [HttpGet("franchise")]
    [HttpGet("Home/Franchise")]
    public IActionResult Franchise()
    {
        return View();
    }

    [HttpPost("franchise")]
    [HttpPost("Home/Franchise")]
    [ValidateAntiForgeryToken]
    public IActionResult Franchise(string fullName, string email, string phone, string proposedLocation, string experience, string investmentRange, string message)
    {
        TempData["FranchiseSuccess"] = $"Thank you, {fullName}. Your franchise inquiry for {proposedLocation} has been submitted for evaluation by JMT Management.";
        return RedirectToAction("Franchise");
    }

    [HttpGet("privacy")]
    [HttpGet("Home/Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("sitemap.xml")]
    [ResponseCache(Duration = 86400)]
    public IActionResult SitemapXml()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
        
        void AddUrl(string path, string priority, string changeFreq)
        {
            sb.AppendLine("  <url>");
            sb.AppendLine($"    <loc>{baseUrl}{path}</loc>");
            sb.AppendLine($"    <lastmod>{DateTime.UtcNow:yyyy-MM-dd}</lastmod>");
            sb.AppendLine($"    <changefreq>{changeFreq}</changefreq>");
            sb.AppendLine($"    <priority>{priority}</priority>");
            sb.AppendLine("  </url>");
        }

        AddUrl("/", "1.0", "daily");
        AddUrl("/menu", "0.9", "daily");
        AddUrl("/about", "0.7", "monthly");
        AddUrl("/contact", "0.8", "monthly");
        AddUrl("/franchise", "0.6", "monthly");
        AddUrl("/privacy", "0.3", "yearly");

        foreach (var product in _catalogService.GetAllProducts())
        {
            AddUrl($"/menu/{product.Id}", "0.7", "weekly");
        }

        sb.AppendLine("</urlset>");
        return Content(sb.ToString(), "application/xml", Encoding.UTF8);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

