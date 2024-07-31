namespace OrderManagement.Data.Models;

public record JwtTokenSettings
{
    public int Expiry { get; set; }
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}