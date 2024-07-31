using MediatR;
using OrderManagement.Data.Models.Request.Authentication;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Commands.Account;

public record CreateUserCommand(CreateUserRequest CreateUserRequest) : IRequest<SalesAppResponse>;