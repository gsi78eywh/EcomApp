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
            Description = "Double shot of house-roasted espresso poured over velvety micro-foamed milk, laced with organic Madagascar vanilla and hand-drizzled with burnt butter caramel.",
            Price = 4.95m,
            Category = "Espresso & Hot",
            RoastLevel = "Medium",
            RoastIntensity = 3,
            Origin = "Antioquia, Colombia",
            FlavorNotes = "Buttery Caramel • Vanilla Bean • Golden Honey",
            Process = "Washed",
            BrewGuide = "Espresso Machine / Moka Pot",
            Altitude = "1,750m",
            ImageUrl = "https://images.unsplash.com/photo-1485808191679-5f86510681a2?auto=format&fit=crop&w=800&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 2,
            Name = "Ethiopian Yirgacheffe Reserve (250g)",
            Description = "Single-origin Grade 1 heirloom whole beans with delicate floral jasmine bouquet, candied bergamot, peach nectar, and a crisp, lingering lemon zest finish.",
            Price = 16.50m,
            Category = "Whole Bean Roasts",
            RoastLevel = "Light",
            RoastIntensity = 1,
            Origin = "Gedeo Zone, Yirgacheffe",
            FlavorNotes = "Jasmine Blossom • Bergamot • White Peach",
            Process = "Natural Raised Beds",
            BrewGuide = "Hario V60 / Chemex / Aeropress",
            Altitude = "2,050m – 2,200m",
            ImageUrl = "https://images.unsplash.com/photo-1559056199-641a0ac8b55e?auto=format&fit=crop&w=800&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 3,
            Name = "Velvet Nitro Cold Brew",
            Description = "Slow-steeped for 20 hours in small batches, charged with micro-nitrogen bubbles for a naturally sweet, cascading creamy pour with zero added dairy or sugar.",
            Price = 5.25m,
            Category = "Cold Brew & Iced",
            RoastLevel = "Dark",
            RoastIntensity = 4,
            Origin = "Bourbon & Caturra Blend",
            FlavorNotes = "Dark Chocolate • Toasted Hazelnut • Sweet Molasses",
            Process = "Cold Filtration",
            BrewGuide = "Ready to Drink / Served on Draft",
            Altitude = "1,600m",
            ImageUrl = "https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?auto=format&fit=crop&w=800&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 4,
            Name = "Spanish Cortado",
            Description = "Pure symmetry in a glass: 1:1 ratio of intense ristretto espresso paired with warm, silky textured whole milk to tame acidity without dampening the roast.",
            Price = 4.20m,
            Category = "Espresso & Hot",
            RoastLevel = "Dark",
            RoastIntensity = 4,
            Origin = "Tarrazú, Costa Rica",
            FlavorNotes = "Dark Cocoa • Brown Sugar • Roasted Almond",
            Process = "Honey Process",
            BrewGuide = "Classic 4oz Gibraltar Glass",
            Altitude = "1,850m",
            ImageUrl = "https://images.unsplash.com/photo-1534778101976-62847782c213?auto=format&fit=crop&w=800&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 5,
            Name = "Vanilla Bean Iced Shakerato",
            Description = "Double shot espresso shaken vigorously over artisanal clear ice cubes with organic vanilla bean syrup and oat milk, creating a thick, frothy crema top.",
            Price = 4.85m,
            Category = "Cold Brew & Iced",
            RoastLevel = "Medium",
            RoastIntensity = 2,
            Origin = "Huehuetenango, Guatemala",
            FlavorNotes = "Madagascar Vanilla • Creamy Cacao • Toffee",
            Process = "Washed",
            BrewGuide = "Shaken over Artisanal Ice",
            Altitude = "1,900m",
            ImageUrl = "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?auto=format&fit=crop&w=800&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 6,
            Name = "Sumatra Mandheling Dark Roast (250g)",
            Description = "Bold, earthy, and syrupy body. A timeless wet-hulled Indonesian specialty roast featuring deep nuances of dark bakers cocoa, cedar bark, and warm clove.",
            Price = 15.00m,
            Category = "Whole Bean Roasts",
            RoastLevel = "Dark",
            RoastIntensity = 5,
            Origin = "Lake Toba, North Sumatra",
            FlavorNotes = "Dark Baker's Cocoa • Cedar Smoke • Clove",
            Process = "Giling Basah (Wet Hulled)",
            BrewGuide = "French Press / Cold Brew / Espresso",
            Altitude = "1,500m",
            ImageUrl = "https://images.unsplash.com/photo-1587734195503-904fca47e0e9?auto=format&fit=crop&w=800&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 7,
            Name = "Artisan Almond Croissant",
            Description = "Twice-baked French butter pastry infused with rich almond frangipane cream, topped with toasted sliced California almonds and powdered organic sugar.",
            Price = 3.95m,
            Category = "Bakery & Treats",
            RoastLevel = "N/A",
            RoastIntensity = 0,
            Origin = "Daily Roastery Bakehouse",
            FlavorNotes = "Toasted Almond • French Butter • Sweet Frangipane",
            Process = "48h Lamination",
            BrewGuide = "Pairs perfectly with a Flat White",
            Altitude = "Fresh Daily",
            ImageUrl = "https://images.unsplash.com/photo-1555507036-ab1f4038808a?auto=format&fit=crop&w=800&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 8,
            Name = "Ceremonial Uji Matcha Latte",
            Description = "Stone-ground first harvest ceremonial grade matcha from Uji, Kyoto, whisked with steaming oat milk and a subtle touch of raw organic mountain honey.",
            Price = 5.50m,
            Category = "Espresso & Hot",
            RoastLevel = "N/A",
            RoastIntensity = 0,
            Origin = "Kyoto Prefecture, Japan",
            FlavorNotes = "Earthy Umami • Spring Meadow • Wild Honey",
            Process = "Shade-Grown Stone Milled",
            BrewGuide = "Bamboo Whisked Chasen",
            Altitude = "Single Estate",
            ImageUrl = "https://images.unsplash.com/photo-1536256263959-770b48d82b0a?auto=format&fit=crop&w=800&q=80",
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
