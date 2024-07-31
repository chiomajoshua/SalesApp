// Ignore Spelling: Middleware

using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;
using System.Text.Json;

namespace OrderManagement.Web.Middleware;

/// <summary>
/// Custom Exception Logging Middleware
/// </summary>
public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                if (contextFeature != null)
                {
                    Log.Error($"ExceptionFailure: {JsonSerializer.Serialize(contextFeature.Error)}.");
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        IsSuccessful = false,
                        Message = "",
                        StatusCode = 500
                    }));
                }
            });
        });
    }
}