using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Commands.Account;
using OrderManagement.Core.Commands.Authentication;
using OrderManagement.Data.Models.Request.Authentication;

namespace OrderManagement.Web.Controllers;

public class AuthenticationController : Controller
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public ActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Login));

        var result = await _mediator.Send(new LoginCommand(loginRequest));

        if (!result.Status)
            return RedirectToAction(nameof(Login));

        HttpContext.Session.SetString("_email", loginRequest.Email);
        return RedirectToAction("Index", "Home");
    }

    public ActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserRequest createUserRequest)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Login));

        var result = await _mediator.Send(new CreateUserCommand(createUserRequest));

        if (!result.Status)
            return View();
        return RedirectToAction("Login", "Authentication");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}