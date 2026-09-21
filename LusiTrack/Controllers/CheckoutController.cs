using Microsoft.AspNetCore.Mvc;
using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    public CheckoutController(ICartService cartService, IOrderService orderService)
    {
        _cartService = cartService;
        _orderService = orderService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        if (!cart.Items.Any())
        {
            TempData["ErrorMessage"] = "Your tray is empty. Please select menu items before checking out!";
            return RedirectToAction("Index", "Shop");
        }

        ViewBag.Cart = cart;
        var model = new OrderModel
        {
            Items = cart.Items,
            Subtotal = cart.Subtotal,
            DeliveryFee = cart.ShippingFee,
            DiscountAmount = cart.DiscountAmount,
            TotalAmount = cart.GrandTotal,
            FulfillmentType = cart.IsPickup ? "Pickup" : "Delivery",
            Address = cart.IsPickup ? "Pickup at JMT CAFE Counter, Purok 5 Tabon, Dalaguete" : "Purok 5 Tabon, Dalaguete, Cebu",
            City = "Dalaguete",
            PostalCode = "6022"
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
            TempData["ErrorMessage"] = "Your tray is empty. Please select menu items before checking out!";
            return RedirectToAction("Index", "Shop");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Cart = cart;
            return View("Index", model);
        }

        // Attach cart items and calculate final breakdown
        model.Items = cart.Items.ToList();
        model.Subtotal = cart.Subtotal;
        model.DeliveryFee = model.FulfillmentType == "Pickup" ? 0m : cart.ShippingFee;
        model.DiscountAmount = cart.DiscountAmount;
        model.TotalAmount = Math.Max(0, model.Subtotal + Math.Round(model.Subtotal * 0.05m, 2) + model.DeliveryFee - model.DiscountAmount);
        model.OrderDate = DateTime.Now;
        model.OrderStatus = "Preparing";
        model.PaymentMethod = "GCash";
        model.PaymentStatus = "Paid (GCash Verified)";
        if (string.IsNullOrWhiteSpace(model.GCashReferenceNumber))
        {
            model.GCashReferenceNumber = $"9832{Random.Shared.Next(1000000, 9999999)}";
        }

        // Save order via service (deducts inventory, records timeline)
        var createdOrder = _orderService.CreateOrder(model);

        // Clear the cart
        _cartService.Clear();

        TempData["SuccessMessage"] = $"Order {createdOrder.OrderId} placed successfully!";
        return RedirectToAction("Confirmation", new { orderId = createdOrder.OrderId });
    }

    [HttpGet]
    public IActionResult Confirmation(string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return RedirectToAction("Index", "Shop");
        }

        var order = _orderService.GetOrderById(orderId);
        if (order == null)
        {
            TempData["ErrorMessage"] = $"Order #{orderId} could not be found.";
            return RedirectToAction("Index", "Shop");
        }

        return View(order);
    }
}

