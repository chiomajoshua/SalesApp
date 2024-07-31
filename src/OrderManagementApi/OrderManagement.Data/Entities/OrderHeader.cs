using OrderManagement.Data.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Data.Entities;

public class OrderHeader : TrackedEntity
{
    [Required]
    [MaxLength(50)]
    public string OrderNumber { get; set; } = null!;

    public OrderType OrderType { get; set; } = OrderType.Normal;

    public OrderStatus OrderStatus { get; set; } = OrderStatus.New;
    public DateTime CreateDate { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual ICollection<OrderLine> OrderLine { get; set; } = [];
}