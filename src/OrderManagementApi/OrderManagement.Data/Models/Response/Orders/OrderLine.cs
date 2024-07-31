using OrderManagement.Data.Enumerators;

namespace OrderManagement.Data.Models.Response.Orders;

public record OrderLine
{
    public int LineNumber { get; set; }
    public string ProductCode { get; set; } = null!;

    public ProductType ProductType { get; set; }

    public double CostPrice { get; set; }

    public double SalesPrice { get; set; }

    public int Quantity { get; set; }
}