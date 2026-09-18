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

    public void AddItem(CoffeeProduct product, int quantity = 1)
    {
        var cart = GetCart();
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == product.Id);

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
                Price = product.Price,
                Quantity = quantity,
                ImageUrl = product.ImageUrl
            });
        }

        SaveCart(cart);
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

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
            SaveCart(cart);
        }
    }

    public void RemoveItem(int productId)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            cart.Items.Remove(item);
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

    private void SaveCart(CartViewModel cart)
    {
        Session.SetObject(CartSessionKey, cart);
    }
}
