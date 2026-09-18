using Microsoft.AspNetCore.Mvc;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ICoffeeCatalogService _catalogService;

    public CartController(ICartService cartService, ICoffeeCatalogService catalogService)
    {
        _cartService = cartService;
        _catalogService = catalogService;
    }

    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, int quantity = 1, string? returnUrl = null)
    {
        var product = _catalogService.GetProductById(productId);
        if (product != null)
        {
            _cartService.AddItem(product, Math.Max(1, quantity));
            TempData["SuccessMessage"] = $"Added {product.Name} to your cart!";
        }

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        _cartService.UpdateQuantity(productId, quantity);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(int productId)
    {
        _cartService.RemoveItem(productId);
        TempData["SuccessMessage"] = "Item removed from cart.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Clear()
    {
        _cartService.Clear();
        TempData["SuccessMessage"] = "Your cart has been cleared.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ItemCount()
    {
        return Json(new { count = _cartService.GetTotalItemCount() });
    }
}
