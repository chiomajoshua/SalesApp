using Microsoft.AspNetCore.Identity;

namespace OrderManagement.Data.Entities.Helpers;

public class RoleSeeder
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleSeeder(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRolesAsync()
    {
        var roleExists = await _roleManager.RoleExistsAsync("Admin");
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
        }
    }
}