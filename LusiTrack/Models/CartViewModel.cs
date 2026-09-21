namespace LusiTrack.Models;

public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public string CouponCode { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; } = 0m;
    public bool IsPickup { get; set; } = false;

    public decimal Subtotal => Items.Sum(x => x.TotalPrice);
    public decimal EstimatedTax => Math.Round(Subtotal * 0.05m, 2);
    public decimal ShippingFee
    {
        get
        {
            if (IsPickup || Subtotal == 0 || Subtotal >= 1000m) return 0m;
            return 50.00m; // standard Dalaguete local delivery
        }
    }

    public decimal GrandTotal => Math.Max(0, Subtotal + EstimatedTax + ShippingFee - DiscountAmount);
    public int TotalItemsCount => Items.Sum(x => x.Quantity);
}

