using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Commands.Orders;
using OrderManagement.Core.Queries.Orders;
using OrderManagement.Data.Models.Request.Orders;
using OrderManagement.Data.Models.Response;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;

namespace OrderManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    public OrderController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [Authorize("Admin")]
    [Produces("application/json")]
    [SwaggerResponse(201, type: typeof(SalesAppResponse))]
    [SwaggerResponse(500, type: typeof(SalesAppResponse))]
    [HttpPost, Route("CreateOrderHeader")]
    public async Task<IActionResult> CreateProductRequest(
        [FromBody] CreateOrderHeader createOrderHeader)
    {
        var userId = _contextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
            x.Type == JwtRegisteredClaimNames.Sid);

        ArgumentNullException.ThrowIfNullOrEmpty(nameof(userId.Value));
        var response = await _mediator.Send(new CreateOrderCommand(createOrderHeader, Guid.Parse(userId.Value)));
        return StatusCode(response.ResponseCode, response);
    }

    [Produces("application/json")]
    [SwaggerResponse(200, type: typeof(SalesAppResponse))]
    [SwaggerResponse(400, type: typeof(SalesAppResponse))]
    [HttpGet, Route("GetOrderByOrderNumber/{orderNumber}")]
    public async Task<IActionResult> CreateProductRequest([FromRoute] string orderNumber)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(orderNumber));
        var response = await _mediator.Send(new GetOrderByOrderNumberQuery(orderNumber));
        return StatusCode(response.ResponseCode, response);
    }
}