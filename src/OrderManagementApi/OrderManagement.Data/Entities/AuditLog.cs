// Ignore Spelling: Timestamp

namespace OrderManagement.Data.Entities;

public class AuditLog : TrackedEntity
{
    public Guid PerformedBy { get; set; }
    public string EntityName { get; set; } = null!;
    public string Action { get; set; } = null!;
    public DateTime? Timestamp { get; set; }
}