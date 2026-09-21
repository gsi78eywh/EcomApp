using Microsoft.AspNetCore.Mvc;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public IActionResult Track(string? orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return View(null);
        }

        var order = _orderService.GetOrderById(orderId);
        if (order == null)
        {
            ViewBag.NotFoundMessage = $"We couldn't find an active order matching '{orderId.Trim()}'. Please verify your Tracking ID.";
            ViewBag.SearchedId = orderId.Trim();
            return View(null);
        }

        return View(order);
    }
}
