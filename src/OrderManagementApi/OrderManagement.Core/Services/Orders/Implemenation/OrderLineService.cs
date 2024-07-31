using OrderManagement.Core.Infrastructure.RepositoryServices.Contracts;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Data.Entities;

namespace OrderManagement.Core.Services.Orders.Implemenation;

public class OrderLineService : IOrderLineService
{
    private readonly IRepositoryService<OrderLine> _repositoryService;

    public OrderLineService(IRepositoryService<OrderLine> repositoryService)
    {
        _repositoryService = repositoryService;
    }

    public async Task<List<OrderLine>> GetAllOrderLinesByOrderId(Guid orderId)
    => await _repositoryService.GetAsync(filter: x => x.OrderId == orderId);

    public async Task<List<OrderLine>> GetAllOrderLinesByOrderNumber(string orderNumber)
    => await _repositoryService.GetAsync(filter: x => x.Order.OrderNumber == orderNumber);
}