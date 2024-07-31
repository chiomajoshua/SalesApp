using OrderManagement.Data.Enumerators;

namespace OrderManagement.Data.Models.Request.Orders;

public record CreateOrderLine
{
    public int LineNumber { get; set; }

    public string ProductCode { get; set; } = null!;

    public ProductType ProductType { get; set; } = ProductType.Apparel;

    public double CostPrice { get; set; }

    public double SalesPrice { get; set; }

    public int Quantity { get; set; }
}