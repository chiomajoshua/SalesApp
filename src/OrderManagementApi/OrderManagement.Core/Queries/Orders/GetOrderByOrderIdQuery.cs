using MediatR;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Queries.Orders;

public record GetOrderByOrderIdQuery(Guid OrderId) : IRequest<SalesAppResponse>;