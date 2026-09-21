namespace LusiTrack.Models;

public class AdminDashboardViewModel
{
    public decimal TodayRevenue { get; set; }
    public int TotalOrdersToday { get; set; }
    public int PendingFulfillmentCount { get; set; }
    public int LowStockItemsCount { get; set; }
    public List<OrderModel> RecentOrders { get; set; } = new();
    public List<CoffeeProduct> LowStockProducts { get; set; } = new();
    public List<CoffeeProduct> AllProducts { get; set; } = new();
}
