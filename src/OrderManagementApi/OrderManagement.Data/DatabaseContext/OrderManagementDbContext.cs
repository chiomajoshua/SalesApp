// Ignore Spelling: Accessor

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderManagement.Data.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace OrderManagement.Data.DatabaseContext;

public class OrderManagementDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderManagementDbContext(DbContextOptions dbContextOptions,
                                    IHttpContextAccessor httpContextAccessor)
                                    : base(dbContextOptions)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<OrderHeader> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<OrderHeader>()
            .HasIndex(o => o.OrderNumber)
            .IsUnique();

        builder.Entity<OrderLine>()
            .HasOne(ol => ol.Order)
            .WithMany(o => o.OrderLine)
            .HasForeignKey(ol => ol.OrderId);

        builder.Entity<OrderHeader>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        GenerateAuditLogs();
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }

    private void GenerateAuditLogs()
    {
        var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
            x.Type == JwtRegisteredClaimNames.Sid);

        ArgumentNullException.ThrowIfNullOrEmpty(nameof(userId.Value));

        var modifiedEntities = ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Added
        || e.State == EntityState.Modified
        || e.State == EntityState.Deleted
        || e.State == EntityState.Unchanged)
        .ToList();

        foreach (var modifiedEntity in modifiedEntities)
        {
            var auditLog = new AuditLog
            {
                EntityName = modifiedEntity.Entity.GetType().Name,
                Action = modifiedEntity.State.ToString(),
                Timestamp = DateTime.UtcNow,
                PerformedBy = Guid.Parse(userId.Value)
            };
            AuditLogs.Add(auditLog);
        }
    }
}