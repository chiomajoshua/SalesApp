using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Commands.Orders;
using OrderManagement.Core.Queries.Orders;
using OrderManagement.Data.Models.Request.Orders;
using OrderManagement.Data.Models.Response.Orders;

namespace OrderManagement.Web.Controllers;

public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("_email")))
            return RedirectToAction("Login", "Authentication");
        var orders = await _mediator.Send(new GetAllOrdersQuery());
        return View(orders.ResponseData);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> SearchOrder(string orderNumber)
    {
        SalesOrder? model = null;

        if (!string.IsNullOrEmpty(orderNumber))
        {
            var orders = await _mediator.Send(new GetOrderByOrderNumberQuery(orderNumber));
            return View(orders.ResponseData);
        }

        return View(model);
    }

    public ActionResult CreateOrder()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("_email")))
            return RedirectToAction("Login", "Authentication");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateOrder(CreateOrderHeader createOrderHeader)
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("_email")))
            return RedirectToAction("Login", "Authentication");

        var order = await _mediator.Send(new CreateOrderCommand(createOrderHeader, HttpContext.Session.GetString("_email")));
        if (!order.Status)
            return View();

        return RedirectToAction("Index", "Home");
    }
}