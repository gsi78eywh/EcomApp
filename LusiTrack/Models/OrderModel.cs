using System.ComponentModel.DataAnnotations;

namespace LusiTrack.Models;

public class OrderModel
{
    public string OrderId { get; set; } = $"LUSI-{Random.Shared.Next(100000, 999999)}";

    [Required(ErrorMessage = "Full name is required")]
    [Display(Name = "Full Name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Delivery address is required")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postal code is required")]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Select a payment method")]
    [Display(Name = "Payment Method")]
    public string PaymentMethod { get; set; } = "CreditCard";

    public string? OrderNotes { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public List<CartItem> Items { get; set; } = new();

    public decimal TotalAmount { get; set; }
}
