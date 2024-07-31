namespace OrderManagement.Data.Models.Exceptions;

public class ModelErrorResponse
{
    public ModelErrorResponse()
    {
        IsValid = true;
        ValidationMessages = [];
    }

    public bool IsValid { get; set; }
    public List<string> ValidationMessages { get; set; }
}