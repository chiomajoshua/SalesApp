using MediatR;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Queries.Account;

public record GetUserByEmailQuery(string EmailAddress) : IRequest<SalesAppResponse>;