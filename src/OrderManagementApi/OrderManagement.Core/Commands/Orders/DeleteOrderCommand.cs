using MediatR;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Commands.Orders;

public record DeleteOrderCommand(Guid OrderId) : IRequest<SalesAppResponse>;