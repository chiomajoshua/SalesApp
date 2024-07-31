using MediatR;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Queries.Orders;

public record GetAllOrdersQuery() : IRequest<SalesAppResponse>;