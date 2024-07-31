using OrderManagement.Data.Enumerators;

namespace OrderManagement.Data.Models.Response.Orders;

public record OrderHeader
{
    public string OrderNumber { get; set; } = null!;

    public OrderType OrderType { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public List<OrderLine> OrderLine { get; set; } = [];
}