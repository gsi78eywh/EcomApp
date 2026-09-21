using LusiTrack.Extensions;
using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Services;

public class SessionCartService : ICartService
{
    private const string CartSessionKey = "LusiTrack_Cart";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionCartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session 
        ?? throw new InvalidOperationException("Session is not available.");

    public CartViewModel GetCart()
    {
        var cart = Session.GetObject<CartViewModel>(CartSessionKey);
        return cart ?? new CartViewModel();
    }

    public void AddItem(CoffeeProduct product, int quantity = 1, string size = "Regular", string temperature = "Hot", string milkOption = "Regular Milk", string specialInstructions = "", decimal priceAdjustment = 0m, string flavorSyrup = "None", string sweetnessLevel = "100% Normal", string addOns = "None")
    {
        var cart = GetCart();
        var effectivePrice = product.Price + priceAdjustment;

        var existingItem = cart.Items.FirstOrDefault(i => 
            i.ProductId == product.Id && 
            string.Equals(i.Size, size, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(i.Temperature, temperature, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(i.MilkOption, milkOption, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(i.FlavorSyrup, flavorSyrup, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(i.SweetnessLevel, sweetnessLevel, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(i.AddOns, addOns, StringComparison.OrdinalIgnoreCase));

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = effectivePrice,
                Quantity = quantity,
                ImageUrl = product.ImageUrl,
                Size = size,
                Temperature = temperature,
                MilkOption = milkOption,
                FlavorSyrup = flavorSyrup,
                SweetnessLevel = sweetnessLevel,
                AddOns = addOns,
                SpecialInstructions = specialInstructions
            });
        }

        RecalculateDiscounts(cart);
        SaveCart(cart);
    }

    public void UpdateQuantity(int productId, int quantity, string? itemId = null)
    {
        var cart = GetCart();
        var item = !string.IsNullOrWhiteSpace(itemId)
            ? cart.Items.FirstOrDefault(i => i.ItemId == itemId)
            : cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            if (quantity <= 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            RecalculateDiscounts(cart);
            SaveCart(cart);
        }
    }

    public void RemoveItem(int productId, string? itemId = null)
    {
        var cart = GetCart();
        var item = !string.IsNullOrWhiteSpace(itemId)
            ? cart.Items.FirstOrDefault(i => i.ItemId == itemId)
            : cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
            RecalculateDiscounts(cart);
            SaveCart(cart);
        }
    }

    public void Clear()
    {
        Session.Remove(CartSessionKey);
    }

    public int GetTotalItemCount()
    {
        return GetCart().TotalItemsCount;
    }

    public bool ApplyCoupon(string couponCode)
    {
        var cart = GetCart();
        if (string.IsNullOrWhiteSpace(couponCode)) return false;

        var code = couponCode.Trim().ToUpperInvariant();
        if (code == "LUSI10" || code == "BREW10")
        {
            cart.CouponCode = code;
            cart.DiscountAmount = Math.Round(cart.Subtotal * 0.10m, 2);
            SaveCart(cart);
            return true;
        }
        else if (code == "CEBU50" || code == "WELCOME50")
        {
            cart.CouponCode = code;
            cart.DiscountAmount = Math.Min(cart.Subtotal, 50.00m);
            SaveCart(cart);
            return true;
        }

        return false;
    }

    public void SetFulfillment(bool isPickup)
    {
        var cart = GetCart();
        cart.IsPickup = isPickup;
        SaveCart(cart);
    }

    private static void RecalculateDiscounts(CartViewModel cart)
    {
        if (cart.CouponCode == "LUSI10" || cart.CouponCode == "BREW10")
        {
            cart.DiscountAmount = Math.Round(cart.Subtotal * 0.10m, 2);
        }
        else if (cart.CouponCode == "CEBU50" || cart.CouponCode == "WELCOME50")
        {
            cart.DiscountAmount = Math.Min(cart.Subtotal, 50.00m);
        }
    }

    private void SaveCart(CartViewModel cart)
    {
        Session.SetObject(CartSessionKey, cart);
    }
}

