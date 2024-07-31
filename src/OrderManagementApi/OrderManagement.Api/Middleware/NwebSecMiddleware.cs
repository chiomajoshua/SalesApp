// Ignore Spelling: Nweb Middleware app

namespace OrderManagement.API.Middleware;

/// <summary>
/// OWASP Setup
/// </summary>
public static class NwebSecMiddleware
{
    /// <summary>
    /// Configuration/DI for OWASP
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseNWebSecurity(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            context.Response.GetTypedHeaders().CacheControl =
             new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
             {
                 NoStore = true,
                 NoCache = true,
                 MustRevalidate = true,
                 MaxAge = TimeSpan.FromSeconds(0),
                 Private = true,
             };
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000;includeSubDomains;preload");
            context.Response.Headers.Append("X-Frame-Options", "DENY");
            context.Response.Headers.Append("Pragma", "no-cache");
            await next();
        });

        app.UseXContentTypeOptions();
        app.UseReferrerPolicy(opts => opts.NoReferrer());
        app.UseRedirectValidation(t => t.AllowSameHostRedirectsToHttps());
        app.UseXXssProtection(options => options.EnabledWithBlockMode());
        app.UseCsp(opts => opts
           .BlockAllMixedContent()
           .ScriptSources(s => s.Self())
           .StyleSources(s => s.Self()));
        return app;
    }
}