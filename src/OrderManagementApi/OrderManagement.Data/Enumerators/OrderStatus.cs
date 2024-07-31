using OrderManagement.Data.Enumerators.Config;

namespace OrderManagement.Data.Enumerators;

public enum OrderStatus
{
    [EnumDisplay(Name = "New", Description = "New Orders")]
    New = 1,

    [EnumDisplay(Name = "Processing", Description = "Order Processing")]
    Processing,

    [EnumDisplay(Name = "Complete", Description = "Order Completed and Fulfilled")]
    Complete
}