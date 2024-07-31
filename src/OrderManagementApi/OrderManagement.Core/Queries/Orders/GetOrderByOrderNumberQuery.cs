using MediatR;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Queries.Orders;

public record GetOrderByOrderNumberQuery(string OrderNumber) : IRequest<SalesAppResponse>;