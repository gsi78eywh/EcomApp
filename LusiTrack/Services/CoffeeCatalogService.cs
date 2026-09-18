using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Services;

public class CoffeeCatalogService : ICoffeeCatalogService
{
    private readonly List<CoffeeProduct> _products = new()
    {
        new CoffeeProduct
        {
            Id = 1,
            Name = "Signature Caramel Macchiato",
            Description = "Rich espresso layered with creamy steamed milk and decadent vanilla syrup, topped with artisanal caramel drizzle.",
            Price = 4.95m,
            Category = "Espresso & Hot",
            RoastLevel = "Medium",
            ImageUrl = "https://images.unsplash.com/photo-1485808191679-5f86510681a2?auto=format&fit=crop&w=600&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 2,
            Name = "Ethiopian Yirgacheffe Beans (250g)",
            Description = "Single-origin specialty whole beans with delicate floral jasmine notes, bright bergamot, and sweet citrus finish.",
            Price = 16.50m,
            Category = "Whole Bean Roasts",
            RoastLevel = "Light",
            ImageUrl = "https://images.unsplash.com/photo-1559056199-641a0ac8b55e?auto=format&fit=crop&w=600&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 3,
            Name = "Nitro Cold Brew",
            Description = "Slow-steeped for 20 hours and infused with nitrogen for a naturally sweet, cascading, velvety smooth draft coffee.",
            Price = 5.25m,
            Category = "Cold Brew & Iced",
            RoastLevel = "Dark",
            ImageUrl = "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?auto=format&fit=crop&w=600&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 4,
            Name = "Spanish Cortado",
            Description = "Equal parts double shot of bold espresso and warm silky textured milk to cut the acidity.",
            Price = 4.20m,
            Category = "Espresso & Hot",
            RoastLevel = "Dark",
            ImageUrl = "https://images.unsplash.com/photo-1534778101976-62847782c213?auto=format&fit=crop&w=600&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 5,
            Name = "Vanilla Bean Iced Latte",
            Description = "Handcrafted espresso pulled over cold milk and organic Madagascar vanilla syrup, served over crystal clear ice.",
            Price = 4.85m,
            Category = "Cold Brew & Iced",
            RoastLevel = "Medium",
            ImageUrl = "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?auto=format&fit=crop&w=600&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 6,
            Name = "Sumatra Mandheling Dark Roast (250g)",
            Description = "Full-bodied Indonesian whole bean roast with earthy cedar tones, baker's cocoa, and subtle spicy notes.",
            Price = 15.00m,
            Category = "Whole Bean Roasts",
            RoastLevel = "Dark",
            ImageUrl = "https://images.unsplash.com/photo-1587734195503-904fca47e0e9?auto=format&fit=crop&w=600&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 7,
            Name = "Artisan Almond Croissant",
            Description = "Twice-baked flaky butter croissant filled with fragrant almond frangipane cream and topped with toasted almonds.",
            Price = 3.95m,
            Category = "Bakery & Treats",
            RoastLevel = "N/A",
            ImageUrl = "https://images.unsplash.com/photo-1555507036-ab1f4038808a?auto=format&fit=crop&w=600&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 8,
            Name = "Matcha Green Tea Latte",
            Description = "Ceremonial grade Japanese Uji matcha whisked with steamed oat milk and a touch of raw honey.",
            Price = 5.50m,
            Category = "Espresso & Hot",
            RoastLevel = "N/A",
            ImageUrl = "https://images.unsplash.com/photo-1536256263959-770b48d82b0a?auto=format&fit=crop&w=600&q=80",
            IsFeatured = false
        }
    };

    public IEnumerable<CoffeeProduct> GetAllProducts() => _products;

    public IEnumerable<CoffeeProduct> GetFeaturedProducts() => _products.Where(p => p.IsFeatured);

    public IEnumerable<CoffeeProduct> GetProductsByCategory(string category)
    {
        if (string.Equals(category, "All", StringComparison.OrdinalIgnoreCase))
            return _products;

        return _products.Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
    }

    public CoffeeProduct? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public IEnumerable<string> GetCategories() => _products.Select(p => p.Category).Distinct().ToList();
}
