using System.ComponentModel.DataAnnotations;

namespace LusiTrack.Models;

public class OrderStatusEntry
{
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

public class DeliveryCourierInfo
{
    public string Name { get; set; } = "Junrey \"Kuya Jun\" D.";
    public string Phone { get; set; } = "+63 906 014 7674";
    public string VehicleModel { get; set; } = "Honda Click 125i (Silver/Black)";
    public string PlateNumber { get; set; } = "7G-8821";
    public double Rating { get; set; } = 4.95;
    public int CompletedTrips { get; set; } = 1420;
    public string AvatarUrl { get; set; } = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?auto=format&fit=crop&w=160&q=80";
    public string Status { get; set; } = "En route with insulated carrier";
    public double RoasteryLat { get; set; } = 9.7848734; // JMT CAFE Purok 5 Tabon, Dalaguete
    public double RoasteryLng { get; set; } = 123.4460781;
    public double CustomerLat { get; set; } = 9.7618; // Customer delivery destination (Poblacion, Dalaguete)
    public double CustomerLng { get; set; } = 123.4682;
    public double CurrentLat { get; set; } = 9.7730; // Active courier GPS position
    public double CurrentLng { get; set; } = 123.4560;
    public int EstimatedMinutesRemaining { get; set; } = 11;
    public double DistanceRemainingKm { get; set; } = 2.4;
    public int CurrentSpeedKmh { get; set; } = 38;
}

public class OrderModel
{
    public string OrderId { get; set; } = $"JMT-2026-{Random.Shared.Next(10000, 99999)}";

    [Required(ErrorMessage = "Full name is required")]
    [Display(Name = "Full Name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Delivery Address")]
    public string Address { get; set; } = "Purok 5 Tabon, Dalaguete, Cebu";

    [Display(Name = "City")]
    public string City { get; set; } = "Dalaguete";

    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = "6022";

    [Display(Name = "Fulfillment Option")]
    public string FulfillmentType { get; set; } = "Delivery"; // "Delivery" or "Pickup"

    [Display(Name = "Payment Method")]
    public string PaymentMethod { get; set; } = "GCash"; // Exclusively GCash for 100% secure pre-payment

    [Display(Name = "GCash Transaction Reference")]
    public string GCashReferenceNumber { get; set; } = $"9832{Random.Shared.Next(1000000, 9999999)}";

    public string? OrderNotes { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public List<CartItem> Items { get; set; } = new();

    public decimal Subtotal { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public string OrderStatus { get; set; } = "OutForDelivery"; // Pending, Confirmed, Preparing, Ready, OutForDelivery, Completed
    public string PaymentStatus { get; set; } = "Paid";
    public int EstimatedMinutes { get; set; } = 20;

    public DeliveryCourierInfo? Courier { get; set; } = new();

    public List<OrderStatusEntry> StatusHistory { get; set; } = new()
    {
        new OrderStatusEntry { Status = "Order Received", Description = "Order details validated & logged", Timestamp = DateTime.Now.AddMinutes(-20) },
        new OrderStatusEntry { Status = "Payment Confirmed", Description = "Payment received successfully", Timestamp = DateTime.Now.AddMinutes(-18) },
        new OrderStatusEntry { Status = "Preparing", Description = "Barista & Head Roaster crafted your order", Timestamp = DateTime.Now.AddMinutes(-12) },
        new OrderStatusEntry { Status = "Packed & Sealed", Description = "Sealed in aroma-lock insulated delivery container", Timestamp = DateTime.Now.AddMinutes(-6) },
        new OrderStatusEntry { Status = "Out for Delivery", Description = "Maxim / LusiTrack courier is en route to your address", Timestamp = DateTime.Now.AddMinutes(-2) }
    };
}
