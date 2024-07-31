using OrderManagement.Data.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Data.Entities;

public class OrderLine : TrackedEntity
{
    [Required]
    public int LineNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public string ProductCode { get; set; } = null!;

    public ProductType ProductType { get; set; } = ProductType.Apparel;

    [Required]
    public double CostPrice { get; set; }

    [Required]
    public double SalesPrice { get; set; }

    [Required]
    public int Quantity { get; set; }

    public Guid OrderId { get; set; }
    public virtual OrderHeader Order { get; set; }
}