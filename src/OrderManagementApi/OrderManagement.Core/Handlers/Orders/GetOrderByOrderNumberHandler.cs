using MediatR;
using OrderManagement.Core.Queries.Orders;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Data.Models.Response;
using OrderManagement.Data.Models.Response.Orders;
using OrderManagement.Data.Models.Extensions;

namespace OrderManagement.Core.Handlers.Orders;

public class GetOrderByOrderNumberHandler : IRequestHandler<GetOrderByOrderNumberQuery, SalesAppResponse>
{
    private readonly IOrderService _orderService;

    public GetOrderByOrderNumberHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<SalesAppResponse> Handle(GetOrderByOrderNumberQuery request, CancellationToken cancellationToken)
    {
        var response = await _orderService.GetOrderByOrderNumber(request.OrderNumber);
        return SalesAppResponse.CustomExistsResponse(new SalesOrder
        {
            OrderHeader = response.ToOrderHeader()
        });
    }
}