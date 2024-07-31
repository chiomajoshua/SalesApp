using Microsoft.AspNetCore.Identity;
using OrderManagement.Core.Services.Accounts.Contracts;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Models.Exceptions;

namespace OrderManagement.Core.Services.Accounts.Implementation;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;

    public AccountService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Create(User user, string passWord)
    {
        var result = await _userManager.CreateAsync(user, passWord);
        return result.Succeeded;
    }

    public async Task<User> GetUserByEmail(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new SalesAppException("Username is not provided");

        var result = await _userManager.FindByEmailAsync(userName);
        return result == null ? throw new SalesAppException("User is not found") : await Task.FromResult(result);
    }
}