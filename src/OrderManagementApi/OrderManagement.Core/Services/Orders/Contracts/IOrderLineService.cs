using OrderManagement.Data.Entities;

namespace OrderManagement.Core.Services.Orders.Contracts;

public interface IOrderLineService
{
    Task<List<OrderLine>> GetAllOrderLinesByOrderId(Guid orderId);

    Task<List<OrderLine>> GetAllOrderLinesByOrderNumber(string orderNumber);
}