using MediatR;
using OrderManagement.Core.Commands.Account;
using OrderManagement.Core.Services.Accounts.Contracts;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Handlers.Account;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, SalesAppResponse>
{
    private readonly IAccountService _accountService;

    public CreateUserHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<SalesAppResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var createUserRequest = request.CreateUserRequest;

        var user = new User
        {
            Email = createUserRequest.Email,
            NormalizedEmail = createUserRequest.Email,
            NormalizedUserName = createUserRequest.Email,
            EmailConfirmed = true,
            FirstName = createUserRequest.FirstName,
            LastName = createUserRequest.LastName,
            UserName = createUserRequest.Email,
            DisplayName = $"{createUserRequest.FirstName},{createUserRequest.LastName}"
        };

        if (!await _accountService.Create(user, createUserRequest.Password))
            return SalesAppResponse.BadRequestResponse(createUserRequest, "Bad Request");
        return SalesAppResponse.SuccessResponse();
    }
}