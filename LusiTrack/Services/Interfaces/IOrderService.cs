using LusiTrack.Models;

namespace LusiTrack.Services.Interfaces;

public interface IOrderService
{
    OrderModel CreateOrder(OrderModel order);
    OrderModel? GetOrderById(string orderId);
    IEnumerable<OrderModel> GetAllOrders();
    bool UpdateOrderStatus(string orderId, string newStatus);
    AdminDashboardViewModel GetDashboardStats();
}
