using MediatR;
using OrderManagement.Core.Queries.Orders;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Data.Models.Response;
using OrderManagement.Data.Models.Response.Orders;
using OrderManagement.Data.Models.Extensions;

namespace OrderManagement.Core.Handlers.Orders;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, SalesAppResponse>
{
    private readonly IOrderService _orderService;

    public GetAllOrdersHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<SalesAppResponse> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var response = await _orderService.GetAllOrders();
        return SalesAppResponse.CustomExistsResponse(new SalesOrders
        {
            OrderHeaders = response.ToOrderHeaders()
        });
    }
}