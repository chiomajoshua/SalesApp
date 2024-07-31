using MediatR;
using OrderManagement.Core.Queries.Account;
using OrderManagement.Core.Services.Accounts.Contracts;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Handlers.Account;

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, SalesAppResponse>
{
    private readonly IAccountService _accountService;

    public GetUserByEmailHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<SalesAppResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _accountService.GetUserByEmail(request.EmailAddress);
        return SalesAppResponse.CustomExistsResponse(user);
    }
}