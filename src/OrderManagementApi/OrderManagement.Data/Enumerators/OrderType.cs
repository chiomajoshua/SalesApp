using OrderManagement.Data.Enumerators.Config;

namespace OrderManagement.Data.Enumerators;

public enum OrderType
{
    [EnumDisplay(Name = "Normal", Description = "Normal Order")]
    Normal = 1,

    [EnumDisplay(Name = "Staff", Description = "Staff Order")]
    Staff,

    [EnumDisplay(Name = "Mechanical", Description = "Mechanical Order")]
    Mechanical,

    [EnumDisplay(Name = "Perishable", Description = "Perishable Order")]
    Perishable
}