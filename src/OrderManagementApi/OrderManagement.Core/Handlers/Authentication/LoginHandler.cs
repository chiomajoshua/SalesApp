using MediatR;
using OrderManagement.Core.Commands.Authentication;
using OrderManagement.Core.Services.Authentication.Contracts;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Handlers.Authentication;

public class LoginHandler : IRequestHandler<LoginCommand, SalesAppResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SalesAppResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginRequest = request.LoginRequest;
        var result = await _authenticationService.Authenticate(loginRequest.Email, loginRequest.Password);
        if (!result)
            return SalesAppResponse.UnAuthorizedResponse();
        return SalesAppResponse.SuccessResponse(result);
    }
}