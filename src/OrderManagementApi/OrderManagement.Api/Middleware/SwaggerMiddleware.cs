using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace OrderManagement.API.Middleware;

/// <summary>
/// Swagger Extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Setup Swagger
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        _ = services.AddSwaggerGen(c =>
        {
            c.MapType<TimeOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "time",
                Example = new OpenApiString("12:30")
            });
            c.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString("2022-01-01")
            });
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "LIP.WebCore",
                Version = "1.0",
                Description = "Love In Planning API Services",
                Contact = new OpenApiContact
                {
                    Name = "HzInfrastructure",
                    Email = "techsupport@hzinfra.ng",
                    Url = new Uri("https://hzinfra.ng")
                },
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.ApiKey
            });
            c.CustomSchemaIds(x => x.FullName);
            c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{e.HttpMethod}");
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
              });
        });
        return services;
    }

    /// <summary>
    /// Run Swagger
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app)
    {
        app.UseSwagger(c => c.SerializeAsV2 = true);
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("./v1/swagger.json", "LIP.WebCore API V1");
        });
        return app;
    }
}