using OrderManagement.Core.Infrastructure.RepositoryServices.Contracts;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Extensions;
using System.Linq.Expressions;

namespace OrderManagement.Core.Services.Orders.Implemenation;

public class OrderService : IOrderService
{
    private readonly IRepositoryService<OrderHeader> _repositoryService;

    public OrderService(IRepositoryService<OrderHeader> repositoryService)
    {
        _repositoryService = repositoryService;
    }

    public async Task<List<OrderHeader>> GetAllOrders()
    {
        Expression<Func<OrderHeader, bool>>? query_filter = null;
        return await _repositoryService.GetAsync(query_filter, includeFunc: query => query.ExtendOrderHeaderIncludes(), orderBy: order => order.CreateDate);
    }

    public async Task<OrderHeader> GetOrderById(Guid id)
    => await _repositoryService.GetSingleAsync(filter: x => x.Id == id, includeFunc: query => query.ExtendOrderHeaderIncludes());

    public async Task<OrderHeader> GetOrderByOrderNumber(string orderNumber)
    => await _repositoryService.GetSingleAsync(filter: x => x.OrderNumber == orderNumber, includeFunc: query => query.ExtendOrderHeaderIncludes());
}