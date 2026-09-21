using LusiTrack.Models;

namespace LusiTrack.Services.Interfaces;

public interface ICartService
{
    CartViewModel GetCart();
    void AddItem(CoffeeProduct product, int quantity = 1, string size = "Regular", string temperature = "Hot", string milkOption = "Regular Milk", string specialInstructions = "", decimal priceAdjustment = 0m, string flavorSyrup = "None", string sweetnessLevel = "100% Normal", string addOns = "None");
    void UpdateQuantity(int productId, int quantity, string? itemId = null);
    void RemoveItem(int productId, string? itemId = null);
    void Clear();
    int GetTotalItemCount();
    bool ApplyCoupon(string couponCode);
    void SetFulfillment(bool isPickup);
}

