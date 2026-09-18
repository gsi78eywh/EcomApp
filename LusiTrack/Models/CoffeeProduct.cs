namespace LusiTrack.Models;

public class CoffeeProduct
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = "Coffee";
    public string RoastLevel { get; set; } = "Medium";
    public int RoastIntensity { get; set; } = 3; // 1 to 5 scale
    public string Origin { get; set; } = string.Empty;
    public string FlavorNotes { get; set; } = string.Empty;
    public string Process { get; set; } = "Washed";
    public string BrewGuide { get; set; } = "Pour-Over / Espresso";
    public string Altitude { get; set; } = "1,800m";
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public bool IsFeatured { get; set; }
}
