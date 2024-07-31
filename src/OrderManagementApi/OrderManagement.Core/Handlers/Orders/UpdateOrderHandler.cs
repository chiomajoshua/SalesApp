using MediatR;
using OrderManagement.Core.Commands.Orders;
using OrderManagement.Core.Services.DatabaseTransactions.Contracts;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Models.Exceptions;
using OrderManagement.Data.Models.Extensions;
using OrderManagement.Data.Models.Request.Orders;
using OrderManagement.Data.Models.Response;
using OrderManagement.Data.Validators.Orders;

namespace OrderManagement.Core.Handlers.Orders;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, SalesAppResponse>
{
    private readonly IDatabaseTransactionService _databaseTransactionService;
    private readonly IOrderService _orderService;

    public UpdateOrderHandler(IDatabaseTransactionService databaseTransactionService,
                             IOrderService orderService)
    {
        _databaseTransactionService = databaseTransactionService;
        _orderService = orderService;
    }

    public async Task<SalesAppResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderRequest = request.UpdateOrderHeader;

        var requestValidation = ValidateUpdateOrderRequest(orderRequest);
        if (!requestValidation.IsValid)
            return SalesAppResponse.BadRequestResponse(orderRequest, requestValidation.ValidationMessages.FirstOrDefault());

        var order = await _orderService.GetOrderById(request.OrderId);
        if (order is null)
            return SalesAppResponse.CustomExistsResponse((OrderHeader?)null);

        var response = await _databaseTransactionService.UpdateOrder(orderRequest.ToOrderHeader(order));
        if (!response)
            return SalesAppResponse.ErrorResponse("Failed to update order");
        return SalesAppResponse.CreatedResponse("Order successfully updated");
    }

    private static ModelErrorResponse ValidateUpdateOrderRequest(UpdateOrderHeader updateOrderHeader)
    {
        var validationMessages = new List<string>();
        var validationResult = new UpdateOrderHeaderValidator().Validate(updateOrderHeader);
        var validationResponse = new ModelErrorResponse();
        if (!validationResult.IsValid)
        {
            validationResponse.IsValid = false;
            validationMessages.AddRange(validationResult.Errors.Select(failure => failure.ErrorMessage));
            validationResponse.ValidationMessages = validationMessages;
        }
        return validationResponse;
    }
}