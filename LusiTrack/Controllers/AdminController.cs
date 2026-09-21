using Microsoft.AspNetCore.Mvc;
using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class AdminController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICoffeeCatalogService _catalogService;
    private const string AdminSessionKey = "IsAdminAuthenticated";
    private const string DefaultAdminPasscode = "JMT2026";

    public AdminController(IOrderService orderService, ICoffeeCatalogService catalogService)
    {
        _orderService = orderService;
        _catalogService = catalogService;
    }

    private bool IsAuthenticated()
    {
        return HttpContext.Session.GetString(AdminSessionKey) == "true";
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (IsAuthenticated())
        {
            return RedirectToAction("Index");
        }
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string passcode, string? returnUrl = null)
    {
        if (!string.IsNullOrWhiteSpace(passcode) && passcode.Trim() == DefaultAdminPasscode)
        {
            HttpContext.Session.SetString(AdminSessionKey, "true");
            TempData["SuccessMessage"] = "Manager session authenticated successfully.";
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index");
        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "Invalid Manager Passcode. Access denied.";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove(AdminSessionKey);
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
