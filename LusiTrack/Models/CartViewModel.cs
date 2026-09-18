namespace LusiTrack.Models;

public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new();

    public decimal Subtotal => Items.Sum(x => x.TotalPrice);
    public decimal EstimatedTax => Math.Round(Subtotal * 0.08m, 2);
    public decimal ShippingFee => Subtotal > 35 || Subtotal == 0 ? 0m : 4.99m;
    public decimal GrandTotal => Subtotal + EstimatedTax + ShippingFee;
    public int TotalItemsCount => Items.Sum(x => x.Quantity);
}
