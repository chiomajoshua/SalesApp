using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Data.Models.Request.Authentication;

public record LoginRequest
{
    [EmailAddress]
    public required string Email { get; init; }
    public required string Password { get; init; }
}