using MediatR;
using OrderManagement.Core.Queries.Orders;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Data.Models.Extensions;
using OrderManagement.Data.Models.Response;
using OrderManagement.Data.Models.Response.Orders;

namespace OrderManagement.Core.Handlers.Orders;

public class GetOrderByOrderIdHandler : IRequestHandler<GetOrderByOrderIdQuery, SalesAppResponse>
{
    private readonly IOrderService _orderService;

    public GetOrderByOrderIdHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<SalesAppResponse> Handle(GetOrderByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _orderService.GetOrderById(request.OrderId);
        return SalesAppResponse.CustomExistsResponse(new SalesOrder
        {
            OrderHeader = response.ToOrderHeader()
        });
    }
}