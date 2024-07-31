using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Data.DatabaseContext;
using OrderManagement.Data.Entities;

namespace OrderManagement.Data.ServiceExtensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(nameof(services));

		var connectionString = configuration.GetConnectionString("DefaultConnection");
		ArgumentNullException.ThrowIfNullOrEmpty(nameof(connectionString));
		services.AddDbContext<OrderManagementDbContext>(options => options.UseSqlServer(connectionString));

		services.AddIdentity<User, IdentityRole<Guid>>()
		   .AddEntityFrameworkStores<OrderManagementDbContext>()
		   .AddDefaultTokenProviders();

		return services;
	}
}