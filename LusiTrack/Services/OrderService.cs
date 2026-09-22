using LusiTrack.Models;
using LusiTrack.Services.Interfaces;

namespace LusiTrack.Services;

public class OrderService : IOrderService
{
    private readonly object _lock = new();
    private readonly ICoffeeCatalogService _catalogService;
    private readonly List<OrderModel> _orders = new();

    public OrderService(ICoffeeCatalogService catalogService)
    {
        _catalogService = catalogService;
        SeedDemoOrders();
    }

    private void SeedDemoOrders()
    {
        _orders.Add(new OrderModel
        {
            OrderId = "JMT-2026-00124",
            CustomerName = "Maria Santos",
            Email = "maria.santos@example.com",
            Phone = "+63 917 123 4567",
            Address = "Poblacion, Dalaguete, Cebu",
            City = "Dalaguete",
            PostalCode = "6022",
            FulfillmentType = "Delivery",
            PaymentMethod = "GCash",
            OrderStatus = "OutForDelivery",
            PaymentStatus = "Paid",
            Subtotal = 380.00m,
            DeliveryFee = 50.00m,
            DiscountAmount = 0m,
            TotalAmount = 430.00m,
            OrderDate = DateTime.Now.AddMinutes(-32),
            Courier = new DeliveryCourierInfo
            {
                Name = "Junrey \"Kuya Jun\" D.",
                Phone = "+63 906 014 7674",
                VehicleModel = "Honda Click 125i (Silver/Black)",
                PlateNumber = "7G-8821",
                Rating = 4.95,
                CompletedTrips = 1420,
                Status = "En route in Dalaguete highlands",
                RoasteryLat = 9.7848734,
                RoasteryLng = 123.4460781,
                CustomerLat = 9.7618,
                CustomerLng = 123.4682,
                CurrentLat = 9.7730,
                CurrentLng = 123.4560,
                EstimatedMinutesRemaining = 8,
                DistanceRemainingKm = 1.6,
                CurrentSpeedKmh = 36
            },
            Items = new()
            {
                new CartItem { ProductId = 1, ProductName = "Giant JMT 10-Inch Burger", Price = 879.00m, Quantity = 1, Size = "10-Inch Giant", Temperature = "N/A" },
                new CartItem { ProductId = 14, ProductName = "Blueberry Mojito", Price = 35.00m, Quantity = 2, Size = "Stemmed Glass (16 oz)", Temperature = "Iced" }
            },
            StatusHistory = new()
            {
                new OrderStatusEntry { Status = "Order Received", Description = "Order logged in system", Timestamp = DateTime.Now.AddMinutes(-32) },
                new OrderStatusEntry { Status = "Payment Confirmed", Description = "GCash transaction ref #983281 approved", Timestamp = DateTime.Now.AddMinutes(-30) },
                new OrderStatusEntry { Status = "Preparing", Description = "Cooked fresh to order", Timestamp = DateTime.Now.AddMinutes(-18) },
                new OrderStatusEntry { Status = "Ready", Description = "Packed in insulated tote", Timestamp = DateTime.Now.AddMinutes(-10) },
                new OrderStatusEntry { Status = "OutForDelivery", Description = "Courier Kuya Junrey dispatched with order", Timestamp = DateTime.Now.AddMinutes(-5) }
            }
        });

        _orders.Add(new OrderModel
        {
            OrderId = "JMT-2026-00120",
            CustomerName = "Carlos Mendoza",
            Email = "carlos.mendoza@example.com",
            Phone = "+63 920 987 6543",
            Address = "Pickup at JMT CAFE Counter",
            City = "Dalaguete",
            PostalCode = "6022",
            FulfillmentType = "Pickup",
            PaymentMethod = "CashOnPickup",
            OrderStatus = "Ready",
            PaymentStatus = "Pending Pickup",
            Subtotal = 428.00m,
            DeliveryFee = 0m,
            DiscountAmount = 0m,
            TotalAmount = 428.00m,
            OrderDate = DateTime.Now.AddHours(-1),
            Courier = null,
            Items = new()
            {
                new CartItem { ProductId = 7, ProductName = "American Burger Combo w/ Fries & Drink", Price = 139.00m, Quantity = 2, Size = "Single Combo" },
                new CartItem { ProductId = 14, ProductName = "Blueberry Mojito", Price = 35.00m, Quantity = 2, Size = "Stemmed Glass (16 oz)", Temperature = "Iced" }
            },
            StatusHistory = new()
            {
                new OrderStatusEntry { Status = "Order Received", Description = "Pickup order placed", Timestamp = DateTime.Now.AddHours(-1) },
                new OrderStatusEntry { Status = "Preparing", Description = "Dishes & drinks cooked fresh to order", Timestamp = DateTime.Now.AddMinutes(-45) },
                new OrderStatusEntry { Status = "Ready", Description = "Order packaged and waiting at JMT Cafe counter for pickup", Timestamp = DateTime.Now.AddMinutes(-15) }
            }
        });
    }

    public OrderModel CreateOrder(OrderModel order)
    {
        lock (_lock)
        {
            order.OrderDate = DateTime.Now;
            if (string.IsNullOrWhiteSpace(order.OrderId))
            {
                order.OrderId = $"JMT-2026-{Random.Shared.Next(10000, 99999)}";
            }

            if (order.FulfillmentType == "Delivery")
            {
                order.Courier = new DeliveryCourierInfo();
            }

            order.StatusHistory = new()
            {
                new OrderStatusEntry { Status = "Order Received", Description = "Order details submitted & verified", Timestamp = DateTime.Now },
                new OrderStatusEntry { Status = "Payment Confirmed", Description = $"Payment method {order.PaymentMethod} approved", Timestamp = DateTime.Now.AddSeconds(10) },
                new OrderStatusEntry { Status = "Preparing", Description = "Kitchen and Baristas assigned to craft your order", Timestamp = DateTime.Now.AddSeconds(20) }
            };

            // Deduct inventory
            foreach (var item in order.Items)
            {
                _catalogService.UpdateStock(item.ProductId, -item.Quantity);
            }

            _orders.Insert(0, order);
            return order;
        }
    }

    public OrderModel? GetOrderById(string orderId)
    {
        lock (_lock)
        {
            if (string.IsNullOrWhiteSpace(orderId)) return null;
            var cleanId = orderId.Trim().ToUpperInvariant();
            return _orders.FirstOrDefault(o => o.OrderId.Equals(cleanId, StringComparison.OrdinalIgnoreCase));
        }
    }

    public IEnumerable<OrderModel> GetAllOrders()
    {
        lock (_lock)
        {
            return _orders.OrderByDescending(o => o.OrderDate).ToList();
        }
    }

    public bool UpdateOrderStatus(string orderId, string newStatus)
    {
        lock (_lock)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId.Equals(orderId, StringComparison.OrdinalIgnoreCase));
            if (order == null) return false;

            order.OrderStatus = newStatus;
            order.StatusHistory.Add(new OrderStatusEntry
            {
                Status = newStatus,
                Description = GetStatusDescription(newStatus, order.FulfillmentType),
                Timestamp = DateTime.Now
            });

            return true;
        }
    }

    private static string GetStatusDescription(string status, string fulfillmentType) => status switch
    {
        "Confirmed" => "Order confirmed by JMT Cafe team.",
        "Preparing" => "Order is being freshly prepared in the JMT Cafe kitchen & espresso bar.",
        "Ready" => fulfillmentType == "Pickup" ? "Order is ready for pickup at the JMT Cafe counter (Purok 5 Tabon)." : "Order packaged in thermal bag, handed to courier.",
        "OutForDelivery" => "Courier is en route to your Dalaguete / Southern Cebu delivery address.",
        "Completed" => "Order fulfilled and enjoyed successfully.",
        "Cancelled" => "Order has been cancelled.",
        _ => "Order status updated."
    };  

    public AdminDashboardViewModel GetDashboardStats()
    {
        lock (_lock)
        {
            var allProducts = _catalogService.GetAllProducts().ToList();
            var lowStock = allProducts.Where(p => p.StockQuantity <= 15).ToList();
            var today = DateTime.Today;
            var todayOrders = _orders.Where(o => o.OrderDate.Date == today).ToList();

            return new AdminDashboardViewModel
            {
                TodayRevenue = _orders.Sum(o => o.TotalAmount),
                TotalOrdersToday = _orders.Count,
                PendingFulfillmentCount = _orders.Count(o => o.OrderStatus != "Completed" && o.OrderStatus != "Cancelled"),
                LowStockItemsCount = lowStock.Count,
                RecentOrders = _orders.Take(10).ToList(),
                LowStockProducts = lowStock,
                AllProducts = allProducts
            };
        }
    }
}
