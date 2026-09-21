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
    public IActionResult AddToCart(
        int productId, 
        int quantity = 1, 
        string size = "Regular", 
        string temperature = "Hot", 
        string milkOption = "Regular Milk", 
        string specialInstructions = "", 
        decimal priceAdjustment = 0m, 
        string flavorSyrup = "None", 
        string sweetnessLevel = "100% Normal", 
        string addOns = "None",
        string actionType = "cart",
        string? returnUrl = null)
    {
        var product = _catalogService.GetProductById(productId);
        if (product != null)
        {
            _cartService.AddItem(product, Math.Max(1, quantity), size, temperature, milkOption, specialInstructions, priceAdjustment, flavorSyrup, sweetnessLevel, addOns);
            
            var variantParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(size) && size != "N/A") variantParts.Add(size);
            if (!string.IsNullOrWhiteSpace(temperature) && temperature != "N/A") variantParts.Add(temperature);
            if (!string.IsNullOrWhiteSpace(milkOption) && milkOption != "Regular Milk" && milkOption != "N/A") variantParts.Add(milkOption);
            if (!string.IsNullOrWhiteSpace(flavorSyrup) && flavorSyrup != "None") variantParts.Add(flavorSyrup);
            if (!string.IsNullOrWhiteSpace(addOns) && addOns != "None") variantParts.Add(addOns);
            if (!string.IsNullOrWhiteSpace(sweetnessLevel) && sweetnessLevel != "100% Normal") variantParts.Add(sweetnessLevel);
            var variantDesc = variantParts.Count > 0 ? string.Join(" • ", variantParts) : "Standard";

            TempData["SuccessMessage"] = $"Added {product.Name} ({variantDesc}) to your order!";

            // Direct Order button triggers immediate navigation to Checkout!
            if (string.Equals(actionType, "order", StringComparison.OrdinalIgnoreCase) || 
                string.Equals(actionType, "checkout", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Checkout");
            }

            // Check if AJAX request
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.Headers.Accept.ToString().Contains("application/json"))
            {
                return Json(new
                {
                    success = true,
                    productName = product.Name,
                    variant = variantDesc,
                    price = $"₱{(product.Price + priceAdjustment):N2}",
                    totalCount = _cartService.GetTotalItemCount()
                });
            }
        }

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int quantity, string? itemId = null)
    {
        _cartService.UpdateQuantity(productId, quantity, itemId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(int productId, string? itemId = null)
    {
        _cartService.RemoveItem(productId, itemId);
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

    [HttpPost]
    public IActionResult ApplyCoupon(string couponCode)
    {
        var success = _cartService.ApplyCoupon(couponCode);
        if (success)
        {
            TempData["SuccessMessage"] = $"Promo code '{couponCode.ToUpperInvariant()}' successfully applied!";
        }
        else
        {
            TempData["ErrorMessage"] = "Invalid promo code. Try 'LUSI10' for 10% off or 'WELCOME50' for ₱50 off.";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SetFulfillment(bool isPickup)
    {
        _cartService.SetFulfillment(isPickup);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ItemCount()
    {
        return Json(new { count = _cartService.GetTotalItemCount() });
    }
}

