using MediatR;
using OrderManagement.Data.Models.Request.Authentication;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Commands.Authentication;

public record LoginCommand(LoginRequest LoginRequest) : IRequest<SalesAppResponse>;