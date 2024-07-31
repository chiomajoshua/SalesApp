using OrderManagement.Data.Enumerators;

namespace OrderManagement.Data.Models.Request.Orders;

public record UpdateOrderHeader
{
    public OrderType OrderType { get; set; } = OrderType.Normal;
    public DateTime CreateDate { get; set; }
    public List<UpdateOrderLine> OrderLine { get; set; } = [];
}