using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Queries.Orders;

namespace OrderManagement.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("getOrderHeaderByOrderNumber/{orderNumber}")]
    public async Task<IActionResult> GetOrderHeaderByOrderNumber(string orderNumber)
    {
        if (string.IsNullOrEmpty(orderNumber))
            return BadRequest();

        return Ok(await _mediator.Send(new GetOrderByOrderNumberQuery(orderNumber)));
    }
}