namespace OrderManagement.Data.Models.Response.Orders;

public record SalesOrder
{
    public OrderHeader? OrderHeader { get; set; }
}