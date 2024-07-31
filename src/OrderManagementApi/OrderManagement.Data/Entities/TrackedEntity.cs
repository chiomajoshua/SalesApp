using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Data.Entities;

public abstract class TrackedEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}