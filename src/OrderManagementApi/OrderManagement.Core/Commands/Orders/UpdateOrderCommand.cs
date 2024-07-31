using MediatR;
using OrderManagement.Data.Models.Request.Orders;
using OrderManagement.Data.Models.Response;

namespace OrderManagement.Core.Commands.Orders;

public record UpdateOrderCommand(UpdateOrderHeader UpdateOrderHeader, Guid OrderId) : IRequest<SalesAppResponse>;