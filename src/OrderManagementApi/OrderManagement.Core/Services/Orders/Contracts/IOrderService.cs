using OrderManagement.Data.Entities;

namespace OrderManagement.Core.Services.Orders.Contracts;

public interface IOrderService
{
    Task<List<OrderHeader>> GetAllOrders();

    Task<OrderHeader> GetOrderById(Guid id);

    Task<OrderHeader> GetOrderByOrderNumber(string orderNumber);
}