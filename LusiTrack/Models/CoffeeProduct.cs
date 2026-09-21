namespace LusiTrack.Models;

public class CoffeeProduct
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = "Coffee";
    public string RoastLevel { get; set; } = "Medium";
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public bool IsFeatured { get; set; }
    public double Rating { get; set; } = 4.8;
    public int ReviewCount { get; set; } = 24;
    public int StockQuantity { get; set; } = 50;
    public string Origin { get; set; } = "Highland Single-Origin";
    public List<string> TastingNotes { get; set; } = new();
    public List<string> AvailableSizes { get; set; } = new() { "Regular", "Large" };
    public List<string> AvailableTemperatures { get; set; } = new();
    public string Ingredients { get; set; } = "100% Specialty Grade Arabica Coffee";
    public string Allergens { get; set; } = "None";
    public string BrewGuide { get; set; } = "Ideal brew ratio 1:16 at 93°C water temperature.";

    public bool HasRoastLevel => !string.IsNullOrWhiteSpace(RoastLevel) && !RoastLevel.Equals("N/A", StringComparison.OrdinalIgnoreCase);
    public bool IsInStock => StockQuantity > 0 && IsAvailable;
}

