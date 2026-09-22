using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class AdminController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICoffeeCatalogService _catalogService;
    private readonly IConfiguration _configuration;
    private const string AdminSessionKey = "IsAdminAuthenticated";

    public AdminController(
        IOrderService orderService, 
        ICoffeeCatalogService catalogService,
        IConfiguration configuration)
    {
        _orderService = orderService;
        _catalogService = catalogService;
        _configuration = configuration;
    }

    private string ExpectedPasscode => 
        _configuration["Admin:Passcode"] ?? 
        Environment.GetEnvironmentVariable("ADMIN_PASSCODE") ?? 
        "JMT2026";

    private bool IsAuthenticated()
    {
        return HttpContext.Session.GetString(AdminSessionKey) == "true";
    }

    private bool ValidatePasscode(string? passcode)
    {
        if (string.IsNullOrWhiteSpace(passcode)) return false;
        var expected = ExpectedPasscode.Trim();
        var input = passcode.Trim();
        var expectedBytes = Encoding.UTF8.GetBytes(expected);
        var inputBytes = Encoding.UTF8.GetBytes(input);
        if (expectedBytes.Length != inputBytes.Length) return false;
        return CryptographicOperations.FixedTimeEquals(expectedBytes, inputBytes);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (IsAuthenticated())
        {
            return RedirectToAction("Index");
        }
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ShowHint = _configuration.GetValue<bool>("Admin:ShowPasscodeHint", false);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string passcode, string? returnUrl = null)
    {
        if (ValidatePasscode(passcode))
        {
            // Session fixation prevention: reset session ID on authentication
            HttpContext.Session.Clear();
            HttpContext.Session.SetString(AdminSessionKey, "true");
            TempData["SuccessMessage"] = "Manager session authenticated successfully.";
            
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index");
        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ShowHint = _configuration.GetValue<bool>("Admin:ShowPasscodeHint", false);
        ViewBag.ErrorMessage = "Invalid Manager Passcode. Access denied.";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["SuccessMessage"] = "Manager session logged out.";
        return RedirectToAction("Login");
    }

    public IActionResult Index()
    {
        if (!IsAuthenticated())
        {
            return RedirectToAction("Login", new { returnUrl = Url.Action("Index", "Admin") });
        }

        var stats = _orderService.GetDashboardStats();
        return View(stats);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateOrderStatus(string orderId, string newStatus)
    {
        if (!IsAuthenticated())
        {
            return Unauthorized();
        }

        var success = _orderService.UpdateOrderStatus(orderId, newStatus);
        if (success)
        {
            TempData["SuccessMessage"] = $"Order {orderId} status updated to '{newStatus}'.";
        }
        else
        {
            TempData["ErrorMessage"] = $"Failed to update order {orderId}.";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RestockProduct(int productId, int quantity = 25)
    {
        if (!IsAuthenticated())
        {
            return Unauthorized();
        }

        var success = _catalogService.UpdateStock(productId, quantity);
        if (success)
        {
            TempData["SuccessMessage"] = $"Added {quantity} units to product #{productId} stock.";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleAvailability(int productId)
    {
        if (!IsAuthenticated())
        {
            return Unauthorized();
        }

        var success = _catalogService.ToggleAvailability(productId);
        if (success)
        {
            TempData["SuccessMessage"] = $"Toggled product #{productId} availability.";
        }

        return RedirectToAction("Index");
    }
}
