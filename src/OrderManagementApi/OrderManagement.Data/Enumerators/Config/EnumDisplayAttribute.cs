namespace OrderManagement.Data.Enumerators.Config;

/// <summary>
///  Enum Display Attribute Model
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class EnumDisplayAttribute : Attribute
{
    public string Name { get; set; }
    public string Description { get; set; }
}