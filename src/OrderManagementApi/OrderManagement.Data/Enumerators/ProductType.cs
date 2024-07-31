using OrderManagement.Data.Enumerators.Config;

namespace OrderManagement.Data.Enumerators;

public enum ProductType
{
    [EnumDisplay(Name = "Apparel", Description = "Apparel")]
    Apparel = 1,

    [EnumDisplay(Name = "Parts", Description = "Parts")]
    Parts,

    [EnumDisplay(Name = "Equipment", Description = "Equipment")]
    Equipment,

    [EnumDisplay(Name = "Motor", Description = "Motor")]
    Motor
}