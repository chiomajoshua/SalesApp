using MediatR;
using OrderManagement.Core.Commands.Orders;
using OrderManagement.Core.Services.Accounts.Contracts;
using OrderManagement.Core.Services.DatabaseTransactions.Contracts;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Data.Models.Exceptions;
using OrderManagement.Data.Models.Extensions;
using OrderManagement.Data.Models.Request.Orders;
using OrderManagement.Data.Models.Response;
using OrderManagement.Data.Validators.Orders;

namespace OrderManagement.Core.Handlers.Orders;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, SalesAppResponse>
{
    private readonly IDatabaseTransactionService _databaseTransactionService;
    private readonly IOrderService _orderService;
    private readonly IAccountService _accountService;

    public CreateOrderHandler(IDatabaseTransactionService databaseTransactionService,
                             IOrderService orderService, IAccountService accountService)
    {
        _databaseTransactionService = databaseTransactionService;
        _orderService = orderService;
        _accountService = accountService;
    }

    public async Task<SalesAppResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderRequest = request.OrderHeader;

        var requestValidation = ValidateCreateOrderRequest(orderRequest);
        if (!requestValidation.IsValid)
            return SalesAppResponse.BadRequestResponse(orderRequest, requestValidation.ValidationMessages.FirstOrDefault());

        var account = await _accountService.GetUserByEmail(request.EmailAddress);
        if (account is null)
            return SalesAppResponse.ErrorResponse("Failed to create order");

        var order = await _orderService.GetOrderByOrderNumber(orderRequest.OrderNumber);
        if (order is not null)
            return SalesAppResponse.ConflictResponse("Order number already exists");

        var response = await _databaseTransactionService.CreateOrder(orderRequest.ToOrderHeader(account.Id));
        if (!response)
            return SalesAppResponse.ErrorResponse("Failed to create order");
        return SalesAppResponse.CreatedResponse("Order created successfully");
    }

    private static ModelErrorResponse ValidateCreateOrderRequest(CreateOrderHeader createOrderHeader)
    {
        var validationMessages = new List<string>();
        var validationResult = new CreateOrderHeaderValidator().Validate(createOrderHeader);
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