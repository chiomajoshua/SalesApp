// Ignore Spelling: Validator

using FluentValidation;
using OrderManagement.Data.Models.Request.Orders;
using OrderManagement.Data.Validators.Helpers;

namespace OrderManagement.Data.Validators.Orders;

public class CreateOrderHeaderValidator : AbstractValidator<CreateOrderHeader>
{
    public CreateOrderHeaderValidator()
    {
        RuleFor(p => p.OrderNumber).NotEmpty()
                                   .WithErrorCode("orderNumber_required")
                                   .WithMessage("Order Number cannot be empty")
                                   .Must(StringValidators.BeValidInput)
                                   .WithMessage("Please validate your input");

        RuleFor(x => x.OrderType).IsInEnum()
                                   .WithMessage("OrderType must be a valid value.");

        RuleFor(p => p.CreateDate).NotEmpty()
                                .WithErrorCode("createDate_required")
                                .WithMessage("Create Date cannot be empty")
                                .LessThanOrEqualTo(DateTime.Now)
                                .WithMessage("Create Date cannot be future date");

        RuleForEach(x => x.OrderLine).SetValidator(new CreateOrderLineValidator());

        RuleFor(x => x.OrderLine)
            .Must(HaveUniqueAndIncrementalLineNumbers)
            .WithMessage("Line numbers must be unique and incremental starting from 1.")
            .Must(HaveUniqueProductCodes)
            .WithMessage("Product codes must be unique within the order lines.");
    }

    private bool HaveUniqueAndIncrementalLineNumbers(List<CreateOrderLine> orderLines)
    {
        if (orderLines is null || orderLines.Count < 1)
            return true;

        var lineNumbers = orderLines.Select(x => x.LineNumber).OrderBy(x => x).ToList();
        for (int i = 0; i < lineNumbers.Count; i++)
        {
            if (lineNumbers[i] != i + 1)
                return false;
        }
        return true;
    }

    private bool HaveUniqueProductCodes(List<CreateOrderLine> orderLines)
    {
        if (orderLines == null || orderLines.Count == 0)
            return true;

        var productCodes = orderLines.ConvertAll(x => x.ProductCode);
        return productCodes.Distinct().Count() == productCodes.Count;
    }
}