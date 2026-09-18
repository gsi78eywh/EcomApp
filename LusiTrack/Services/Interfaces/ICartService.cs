using LusiTrack.Models;

namespace LusiTrack.Services.Interfaces;

public interface ICartService
{
    CartViewModel GetCart();
    void AddItem(CoffeeProduct product, int quantity = 1);
    void UpdateQuantity(int productId, int quantity);
    void RemoveItem(int productId);
    void Clear();
    int GetTotalItemCount();
}
