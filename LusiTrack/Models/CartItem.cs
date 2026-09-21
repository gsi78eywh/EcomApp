namespace LusiTrack.Models;

public class CartItem
{
    public string ItemId { get; set; } = Guid.NewGuid().ToString("N");
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
    public string ImageUrl { get; set; } = string.Empty;
    public string Size { get; set; } = "Regular";
    public string Temperature { get; set; } = "Hot";
    public string MilkOption { get; set; } = "Regular Milk";
    public string FlavorSyrup { get; set; } = "None";
    public string SweetnessLevel { get; set; } = "100% Normal";
    public string AddOns { get; set; } = "None";
    public string SpecialInstructions { get; set; } = string.Empty;

    public decimal TotalPrice => Price * Quantity;

    public string VariantSummary
    {
        get
        {
            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(Size) && Size != "N/A") parts.Add(Size);
            if (!string.IsNullOrWhiteSpace(Temperature) && Temperature != "N/A") parts.Add(Temperature);
            if (!string.IsNullOrWhiteSpace(MilkOption) && MilkOption != "N/A" && MilkOption != "Regular Milk") parts.Add(MilkOption);
            if (!string.IsNullOrWhiteSpace(FlavorSyrup) && FlavorSyrup != "None") parts.Add(FlavorSyrup);
            if (!string.IsNullOrWhiteSpace(AddOns) && AddOns != "None") parts.Add(AddOns);
            if (!string.IsNullOrWhiteSpace(SweetnessLevel) && SweetnessLevel != "100% Normal" && SweetnessLevel != "100%") parts.Add(SweetnessLevel);
            return parts.Count > 0 ? string.Join(" • ", parts) : string.Empty;
        }
    }
}

