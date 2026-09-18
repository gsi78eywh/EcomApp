using Microsoft.AspNetCore.Mvc;
using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartService _cartService;

    public CheckoutController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        if (!cart.Items.Any())
        {
            TempData["ErrorMessage"] = "Your cart is empty. Please add some coffee items before checking out!";
            return RedirectToAction("Index", "Shop");
        }

        ViewBag.Cart = cart;
        var model = new OrderModel
        {
            Items = cart.Items,
            TotalAmount = cart.GrandTotal
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PlaceOrder(OrderModel model)
    {
        var cart = _cartService.GetCart();
        if (!cart.Items.Any())
        {
            return RedirectToAction("Index", "Shop");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Cart = cart;
            model.Items = cart.Items;
            model.TotalAmount = cart.GrandTotal;
            return View("Index", model);
        }

        // Attach cart items and calculate final total
        model.Items = cart.Items.ToList();
        model.TotalAmount = cart.GrandTotal;
        model.OrderDate = DateTime.Now;

        // Clear the cart after successful order placement
        _cartService.Clear();

        // Pass order details to confirmation via TempData or session
        TempData["LastOrderId"] = model.OrderId;
        TempData["CustomerName"] = model.CustomerName;
        TempData["TotalPaid"] = model.TotalAmount.ToString("C");

        return RedirectToAction("Confirmation", new { orderId = model.OrderId });
    }

    [HttpGet]
    public IActionResult Confirmation(string orderId)
    {
        ViewBag.OrderId = orderId;
        ViewBag.CustomerName = TempData["CustomerName"] ?? "Valued Customer";
        ViewBag.TotalPaid = TempData["TotalPaid"] ?? "$0.00";
        return View();
    }
}
