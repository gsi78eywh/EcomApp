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
        },
        new CoffeeProduct
        {
            Id = 9,
            Name = "Colombia Geisha Reserve (250g)",
            Description = "Award-winning Geisha lot cultivated at 2,000 meters in Huila. Extraordinary cup clarity displaying white tea florals, juicy apricot, and a lavender honey finish.",
            Price = 22.00m,
            Category = "Whole Bean Roasts",
            RoastLevel = "Light",
            RoastIntensity = 1,
            Origin = "Huila, Colombia",
            FlavorNotes = "White Tea • Apricot Nectar • Lavender Honey",
            Process = "Anaerobic Washed",
            BrewGuide = "Origami / Kalita Wave / Chemex",
            Altitude = "1,950m – 2,100m",
            ImageUrl = "https://images.unsplash.com/photo-1511537190424-bbbab87ac5eb?auto=format&fit=crop&w=800&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 10,
            Name = "Guatemala Antigua Peaberry (250g)",
            Description = "Rare natural mutation where a single round peaberry develops inside the cherry. Incredibly dense bean yielding intense cocoa, sweet Meyer lemon, and warming allspice.",
            Price = 17.50m,
            Category = "Whole Bean Roasts",
            RoastLevel = "Medium",
            RoastIntensity = 3,
            Origin = "Antigua Valley, Guatemala",
            FlavorNotes = "Dark Chocolate • Meyer Lemon • Warm Allspice",
            Process = "Sun-Dried Washed",
            BrewGuide = "V60 / Clever Dripper / French Press",
            Altitude = "1,800m",
            ImageUrl = "https://images.unsplash.com/photo-1611854779393-1b2da9d400fe?auto=format&fit=crop&w=800&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 11,
            Name = "Aussie Flat White",
            Description = "Expertly pulled double ristretto topped with free-poured velvety steamed whole milk, creating a paper-thin layer of glossy microfoam.",
            Price = 4.65m,
            Category = "Espresso & Hot",
            RoastLevel = "Medium",
            RoastIntensity = 3,
            Origin = "East African & South American Blend",
            FlavorNotes = "Creamy Praline • Cocoa Powder • Sweet Toffee",
            Process = "Microfoam Steamed",
            BrewGuide = "Traditional 6oz Ceramic Cup",
            Altitude = "1,800m Blend",
            ImageUrl = "https://images.unsplash.com/photo-1577968897966-3d4325b36b61?auto=format&fit=crop&w=800&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 12,
            Name = "Single-Origin Dark Mocha",
            Description = "Double shot espresso fused with artisanal 72% Valrhona single-origin melted chocolate ganache and steamed silky milk, lightly dusted with organic cocoa powder.",
            Price = 5.25m,
            Category = "Espresso & Hot",
            RoastLevel = "Dark",
            RoastIntensity = 4,
            Origin = "Huila Colombia & Ecuadorian Cacao",
            FlavorNotes = "72% Dark Cacao • Roasted Almond • Espresso Crema",
            Process = "Artisan Ganache Melt",
            BrewGuide = "Signature Ceramic Mug",
            Altitude = "1,750m",
            ImageUrl = "https://images.unsplash.com/photo-1578314675249-a6910f80cc4e?auto=format&fit=crop&w=800&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 13,
            Name = "Salted Caramel Cream Cold Brew",
            Description = "Signature 20-hour steeped cold brew coffee served over clear ice, crowned with a thick, decadent layer of salted burnt caramel cold foam.",
            Price = 5.60m,
            Category = "Cold Brew & Iced",
            RoastLevel = "Medium",
            RoastIntensity = 3,
            Origin = "Central American Specialty Blend",
            FlavorNotes = "Maldon Sea Salt • Burnt Caramel • Smooth Cocoa",
            Process = "Cold Foam Layered",
            BrewGuide = "Served Chilled in Highball",
            Altitude = "1,650m",
            ImageUrl = "https://images.unsplash.com/photo-1541167760496-1628856ab772?auto=format&fit=crop&w=800&q=80",
            IsFeatured = true
        },
        new CoffeeProduct
        {
            Id = 14,
            Name = "Cascara & Citrus Cold Brew Tonic",
            Description = "Sparkling Fever-Tree botanical tonic poured over single-origin cold brew and natural coffee cherry cascara syrup, garnished with charred blood orange.",
            Price = 5.40m,
            Category = "Cold Brew & Iced",
            RoastLevel = "Light",
            RoastIntensity = 1,
            Origin = "Yirgacheffe & Botanical Tonic",
            FlavorNotes = "Sparkling Citrus • Hibiscus • Rose Hip",
            Process = "Cascara Reduction",
            BrewGuide = "Served over Crystal Ice Sphere",
            Altitude = "2,000m",
            ImageUrl = "https://images.unsplash.com/photo-1517256064527-09c73fc73e38?auto=format&fit=crop&w=800&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 15,
            Name = "Cardamom Brown Sugar Morning Bun",
            Description = "Croissant laminated dough swirled with freshly cracked organic green cardamom, dark muscovado brown sugar, and sea salt, baked to golden caramelized perfection.",
            Price = 4.25m,
            Category = "Bakery & Treats",
            RoastLevel = "N/A",
            RoastIntensity = 0,
            Origin = "Daily Roastery Bakehouse",
            FlavorNotes = "Green Cardamom • Caramelized Sugar • Flaky Butter",
            Process = "Wild Yeast Sourdough",
            BrewGuide = "Pairs with Pour-Over or Americano",
            Altitude = "Baked Fresh 5:30 AM",
            ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?auto=format&fit=crop&w=800&q=80",
            IsFeatured = false
        },
        new CoffeeProduct
        {
            Id = 16,
            Name = "Espresso Hazelnut Cantucci Biscotti",
            Description = "Traditional twice-baked Tuscan dipping biscuits loaded with roasted Piedmont hazelnuts, dark chocolate chunks, and a hint of house espresso liqueur.",
            Price = 3.50m,
            Category = "Bakery & Treats",
            RoastLevel = "N/A",
            RoastIntensity = 0,
            Origin = "Daily Roastery Bakehouse",
            FlavorNotes = "Piedmont Hazelnut • Dark Chocolate • Crisp Crunch",
            Process = "Double Baked Cantucci",
            BrewGuide = "Crafted for Espresso & Cortado Dipping",
            Altitude = "Small Batch Pantry",
            ImageUrl = "https://images.unsplash.com/photo-1558961363-fa8fdf82db35?auto=format&fit=crop&w=800&q=80",
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

    public IEnumerable<CoffeeProduct> SearchProducts(string? query, string? category = null)
    {
        var baseList = _products.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category) && !string.Equals(category, "All", StringComparison.OrdinalIgnoreCase))
        {
            baseList = baseList.Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        if (string.IsNullOrWhiteSpace(query))
        {
            return baseList;
        }

        var terms = query.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return baseList.Where(p =>
        {
            return terms.All(term =>
                (p.Name != null && p.Name.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                (p.Description != null && p.Description.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                (p.FlavorNotes != null && p.FlavorNotes.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                (p.Origin != null && p.Origin.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                (p.Category != null && p.Category.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                (p.RoastLevel != null && p.RoastLevel.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                (p.BrewGuide != null && p.BrewGuide.Contains(term, StringComparison.OrdinalIgnoreCase))
            );
        });
    }
}
