namespace OrderManagement.Core.Services.Authentication.Contracts;

public interface IAuthenticationService
{
    Task<bool> Authenticate(string email, string password);
}