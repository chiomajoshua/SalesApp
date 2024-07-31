// Ignore Spelling: Jwt Middleware

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using OrderManagement.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace OrderManagement.API.Middleware;

/// <summary>
/// JSON Web Token Configuration
/// </summary>
public static class JwtMiddleware
{
    /// <summary>
    /// Dependency Injection Setup
    /// </summary>
    /// <param name="services"></param>
    /// <param name="jwtToken"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwt(this IServiceCollection services, JwtTokenSettings jwtToken)
    {
        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwt =>
        {
            jwt.RequireHttpsMetadata = true;
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtToken.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        return services;
    }
}