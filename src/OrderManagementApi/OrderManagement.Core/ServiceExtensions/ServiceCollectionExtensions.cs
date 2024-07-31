using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Core.Infrastructure.RepositoryServices.Contracts;
using OrderManagement.Core.Infrastructure.RepositoryServices.Implementation;
using OrderManagement.Core.Services.Accounts.Contracts;
using OrderManagement.Core.Services.Accounts.Implementation;
using OrderManagement.Core.Services.Authentication.Contracts;
using OrderManagement.Core.Services.Authentication.Implementation;
using OrderManagement.Core.Services.DatabaseTransactions.Contracts;
using OrderManagement.Core.Services.DatabaseTransactions.Impementation;
using OrderManagement.Core.Services.Orders.Contracts;
using OrderManagement.Core.Services.Orders.Implemenation;

namespace OrderManagement.Core.ServiceExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));
        services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IOrderLineService, OrderLineService>();
        services.AddTransient<IDatabaseTransactionService, DatabaseTransactionService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IAccountService, AccountService>();

        return services;
    }
}