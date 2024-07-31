namespace OrderManagement.Data.Models.Response.Authentication;

public record LoginResponse
{
    public string? Email { get; set; }
    public string? Role { get; set; }
}