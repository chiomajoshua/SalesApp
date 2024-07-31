using MediatR;
using OrderManagement.Core.Commands.Orders;
using OrderManagement.Core.Services.DatabaseTransactions.Contracts;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Handlers.Orders;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, SalesAppResponse>
{
    private readonly IDatabaseTransactionService _databaseTransactionService;
    private readonly IOrderService _orderService;

    public DeleteOrderHandler(IDatabaseTransactionService databaseTransactionService,
                             IOrderService orderService)
    {
        _databaseTransactionService = databaseTransactionService;
        _orderService = orderService;
    }

    public async Task<SalesAppResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderById(request.OrderId);
        if (order is null)
            return SalesAppResponse.CustomExistsResponse((OrderHeader?)null);

        var response = await _databaseTransactionService.DeleteOrder(order);
        if (!response)
            return SalesAppResponse.ErrorResponse("Failed to delete order");
        return SalesAppResponse.SuccessResponse("Order successfully deleted");
    }
}