using Microsoft.AspNetCore.Identity;
using OrderManagement.Data.Entities;

namespace OrderManagement.Core.Services.Accounts.Contracts;

public interface IAccountService
{
    Task<User> GetUserByEmail(string userName);

    Task<bool> Create(User user, string passWord);
}