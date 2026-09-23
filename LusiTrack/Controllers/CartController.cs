using Microsoft.AspNetCore.Mvc;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Controllers;

[AutoValidateAntiforgeryToken]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ICoffeeCatalogService _catalogService;

    public CartController(ICartService cartService, ICoffeeCatalogService catalogService)
    {
        _cartService = cartService;
        _catalogService = catalogService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        TempData["SuccessMessage"] = "JMT CAFE is a digital viewing showcase. For orders, reservations, and delivery inquiries, please message our official Facebook page or call 0906 014 7674!";
        return RedirectToAction("Index", "Shop");
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
            // Security: clamp quantity and sanitize price adjustment against client manipulation
            var safeQuantity = Math.Clamp(quantity, 1, 99);
            var safeAdjustment = Math.Max(0m, Math.Min(500m, priceAdjustment));

            _cartService.AddItem(
                product, 
                safeQuantity, 
                size, 
                temperature, 
                milkOption, 
                specialInstructions, 
                safeAdjustment, 
                flavorSyrup, 
                sweetnessLevel, 
                addOns);
            
            var variantParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(size) && size != "N/A" && size != "Standard") variantParts.Add(size);
            if (!string.IsNullOrWhiteSpace(temperature) && temperature != "N/A" && temperature != "Standard") variantParts.Add(temperature);
            if (!string.IsNullOrWhiteSpace(milkOption) && milkOption != "Regular Milk" && milkOption != "N/A") variantParts.Add(milkOption);
            if (!string.IsNullOrWhiteSpace(flavorSyrup) && flavorSyrup != "None") variantParts.Add(flavorSyrup);
            if (!string.IsNullOrWhiteSpace(addOns) && addOns != "None") variantParts.Add(addOns);
            if (!string.IsNullOrWhiteSpace(sweetnessLevel) && sweetnessLevel != "100% Normal" && sweetnessLevel != "100%") variantParts.Add(sweetnessLevel);
            var variantDesc = variantParts.Count > 0 ? string.Join(" • ", variantParts) : "Standard";

            TempData["SuccessMessage"] = $"Added {product.Name} ({variantDesc}) to your order!";

            // Direct Order button triggers immediate navigation to Checkout
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
                    price = $"₱{(product.Price + safeAdjustment):N2}",
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
        var safeQuantity = Math.Clamp(quantity, 0, 99);
        _cartService.UpdateQuantity(productId, safeQuantity, itemId);
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
            TempData["ErrorMessage"] = "Invalid promo code. Try 'JMT10' for 10% off or 'DALAGUETE50' for ₱50 off.";
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
