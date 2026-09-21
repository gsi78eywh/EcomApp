namespace LusiTrack.Services;

/// <summary>
/// Centralized Single Source of Truth for JMT CAFE business details, contact info, and NAP data.
/// </summary>
public static class JmtCafeConfig
{
    public const string Name = "JMT CAFE";
    public const string Tagline = "Eat & Chill Tambayan • Coffee, Resto";
    public const string SubTitle = "Eat & Chill Tambayan";
    public const string Established = "Est. 2024";
    public const string LogoUrl = "/images/jmt/jmt_logo.png";
    public const string ContactPerson = "Jonathan Villamor (Jonathan The Explorer)";
    public const string Email = "jonathanvillamor37@gmail.com";
    
    // Exact official NAP (Name, Address, Phone)
    public const string AddressLine1 = "Purok 5 Tabon";
    public const string Barangay = "Tabon";
    public const string Municipality = "Dalaguete";
    public const string Province = "Cebu";
    public const string PostalCode = "6022";
    public const string Country = "Philippines";
    public const string FullAddress = "Purok 5 Tabon, Dalaguete, 6022 Cebu, Philippines";
    
    // Landmarks & Highway Description
    public const string Landmark = "Near Tabon Basketball Court & Purok 6 Mini Mart";
    public const string HighwayDescription = "Highlands of Barangay Tabon along the scenic Dalaguete–Mantalongon road towards Osmeña Peak";
    public const string PlusCode = "QCMW+WC Dalaguete, Cebu";
    
    // Geographic Coordinates (Verified on Google Maps)
    public const double Latitude = 9.7848734;
    public const double Longitude = 123.4460781;
    public const string CoordinatesString = "9.7848734, 123.4460781";
    
    // Phone & Communications (Official from Facebook Page)
    public const string PhoneDisplay = "0906 014 7674";
    public const string PhoneInternational = "+639060147674";
    public const string PhoneTelLink = "tel:09060147674";
    
    // Delivery Policy (Official from Facebook Page)
    public const decimal DeliveryFeeNear = 50m;
    public const decimal DeliveryFeeLungsod = 150m;
    public const string DeliveryDescription = "₱50 near the shop (Tabon) • Max ₱150 Lungsod sa Dalaguete (₱1k order bill)";
    
    // Operating Hours
    public const string OpeningTime = "9:00 AM";
    public const string ClosingTime = "10:00 PM";
    public const string HoursDisplay = "Open Daily: 9:00 AM – 10:00 PM";
    public const string HoursShort = "9:00 AM – 10:00 PM";
    
    // Official Links
    public const string FacebookPageUrl = "https://www.facebook.com/profile.php?id=61566969959363";
    public const string FacebookPageName = "JMT CAFE";
    public const string BisayangExplorerUrl = "https://www.facebook.com/BisayangExplorerTV";
    public const string GoogleMapsPlaceUrl = "https://www.google.com/maps/place/JMT+CAFE/@9.7848734,123.4460781,17z";
    public const string GoogleMapsEmbedUrl = "https://maps.google.com/maps?q=9.7848734,123.4460781&hl=en&z=17&output=embed";
    
    // Pricing Strategy
    public const string PriceRangeDisplay = "₱35 – ₱879";
    public const string DrinksFrom = "₱35";
    public const string MealsFrom = "₱45";
    
    // Services & Environment
    public const bool HasAirConditioning = false;
    public const bool HasMountainBreeze = true;
    public const bool HasRooftopDeck = true;
    public const bool HasFreeWifi = true;
    public const bool HasDineIn = true;
    public const bool HasTakeout = true;
    public const bool HasDelivery = true;
    public const bool HasRoadsideParking = true;
    public const string Ambience = "Open-Air Mountain Breeze & Rooftop Viewing Deck";
    public const string BuildingDescription = "Orange 2-story highland building with scenic open-air rooftop viewing deck";

    // === Menu Categories Metadata ===
    public static readonly IReadOnlyList<LusiTrack.Models.MenuCategoryModel> CategoryDefinitions = new List<LusiTrack.Models.MenuCategoryModel>
    {
        new()
        {
            Key = "Burgers & Snacks",
            DisplayName = "Burgers, Combos & Snacks",
            ShortName = "Burgers & Combos",
            Icon = "bi-stack",
            Emoji = "🍔",
            AnchorId = "cat-burgers",
            BadgeClass = "bg-danger text-white",
            CoverImageUrl = "/images/jmt/jmt_american_burger_combo.png",
            PriceRange = "₱20 – ₱879",
            Description = "Flame-grilled beef patties, value combos with golden fries & chilled drinks, Buy 1 Take 1 deals, and our legendary Giant 10-Inch Party Burger.",
            DisplayOrder = 1
        },
        new()
        {
            Key = "Silog & Rice Meals",
            DisplayName = "Rice Meals & Silogs",
            ShortName = "Rice Meals",
            Icon = "bi-egg-fried",
            Emoji = "🍗",
            AnchorId = "cat-ricemeals",
            BadgeClass = "bg-warning text-dark",
            CoverImageUrl = "/images/jmt/jmt_burger_steak_silog.png",
            PriceRange = "₱45 – ₱135",
            Description = "Crispy golden fried chicken with savory house gravy, CornSilog, LoafSilog, and hearty breakfast favorites served with hot steamed jasmine rice.",
            DisplayOrder = 2
        },
        new()
        {
            Key = "Coffee Drinks",
            DisplayName = "Barista Coffee & Espresso",
            ShortName = "Coffee & Espresso",
            Icon = "bi-cup-hot-fill",
            Emoji = "☕",
            AnchorId = "cat-coffee",
            BadgeClass = "bg-dark text-warning",
            CoverImageUrl = "/images/jmt/jmt_coffee_clock.png",
            PriceRange = "₱39 – ₱65",
            Description = "Espresso poured with artisan latte art (Swan, Tulip, Rosetta), authentic Vietnamese Egg Coffee with golden custard foam, and iced Spanish lattes.",
            DisplayOrder = 3
        },
        new()
        {
            Key = "Mojito Drinks",
            DisplayName = "Sparkling Fruit Mojitos",
            ShortName = "Fruit Mojitos",
            Icon = "bi-cup-straw",
            Emoji = "🍹",
            AnchorId = "cat-mojitos",
            BadgeClass = "bg-info text-dark",
            CoverImageUrl = "/images/jmt/jmt_mojito_menu.png",
            PriceRange = "₱35 – ₱39",
            Description = "Refreshing chilled sparkling fruit sodas infused with crisp mint leaves, fresh lime, crushed ice, and vibrant fruit syrups.",
            DisplayOrder = 4
        },
        new()
        {
            Key = "Juice & Shakes",
            DisplayName = "Fruit & Oreo Dessert Shakes",
            ShortName = "Shakes & Frappes",
            Icon = "bi-cup-fill",
            Emoji = "🥤",
            AnchorId = "cat-shakes",
            BadgeClass = "bg-success text-white",
            CoverImageUrl = "/images/jmt/jmt_burger_shakes.png",
            PriceRange = "₱30 – ₱85",
            Description = "Large 16 oz fresh mango and avocado shakes, plus thick chocolate Oreo dessert frappes topped with cookie crunch in elegant wine goblets.",
            DisplayOrder = 5
        },
        new()
        {
            Key = "Beer & Beverages (18+)",
            DisplayName = "Beers & Refreshments (18+)",
            ShortName = "Beers (18+)",
            Icon = "bi-slash-circle",
            Emoji = "🍺",
            AnchorId = "cat-beers",
            BadgeClass = "bg-secondary text-white",
            CoverImageUrl = "/images/jmt/jmt_storefront_highlands.jpg",
            PriceRange = "₱150",
            Description = "Ice cold beers and chilled refreshments for road-trippers and riders relaxing after conquering Dalaguete's mountain roads.",
            DisplayOrder = 6
        }
    };

    public static LusiTrack.Models.MenuCategoryModel GetCategoryDefinition(string? categoryKey)
    {
        if (string.IsNullOrWhiteSpace(categoryKey))
        {
            return CategoryDefinitions[0];
        }
        return CategoryDefinitions.FirstOrDefault(c => c.Key.Equals(categoryKey, StringComparison.OrdinalIgnoreCase)) 
               ?? new LusiTrack.Models.MenuCategoryModel
               {
                   Key = categoryKey,
                   DisplayName = categoryKey,
                   ShortName = categoryKey,
                   Icon = "bi-tag",
                   Emoji = "🍽️",
                   AnchorId = "cat-" + categoryKey.ToLowerInvariant().Replace(" ", "-").Replace("&", "and"),
                   BadgeClass = "bg-secondary text-white",
                   CoverImageUrl = "/images/jmt/jmt_storefront_highlands.jpg",
                   PriceRange = "Varies",
                   Description = "Specialty offerings crafted fresh at JMT Cafe.",
                   DisplayOrder = 99
               };
    }
}
