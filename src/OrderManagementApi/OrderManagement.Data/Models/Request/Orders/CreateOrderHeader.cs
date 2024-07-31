using OrderManagement.Data.Enumerators;

namespace OrderManagement.Data.Models.Request.Orders;

public record CreateOrderHeader
{
    public string OrderNumber { get; set; } = null!;
    public OrderType OrderType { get; set; } = OrderType.Normal;
    public DateTime CreateDate { get; set; }
    public List<CreateOrderLine> OrderLine { get; set; } = [];
}