using Microsoft.AspNetCore.Identity;

namespace OrderManagement.Data.Entities;

public class User : IdentityUser<Guid>
{
    public User()
    { }

    public User(string? email, string? phoneNumber)
    {
        Email = string.IsNullOrWhiteSpace(email) ? null : email;
        PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber;
        UserName = PhoneNumber ?? Email;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public ICollection<OrderHeader> Orders { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    public override string ToString() => $"{FirstName} {LastName}";
}