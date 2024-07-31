namespace OrderManagement.Data.Models.Response.Orders;

public record SalesOrders
{
    public List<OrderHeader> OrderHeaders { get; set; } = [];
}