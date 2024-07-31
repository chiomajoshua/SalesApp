using Microsoft.AspNetCore.Identity;
using OrderManagement.Core.Services.Authentication.Contracts;
using OrderManagement.Data.Entities;

namespace OrderManagement.Core.Services.Authentication.Implementation;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }
}