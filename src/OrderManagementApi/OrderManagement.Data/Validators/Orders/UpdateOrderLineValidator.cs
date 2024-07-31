// Ignore Spelling: Validator

using FluentValidation;
using OrderManagement.Data.Models.Request.Orders;
using OrderManagement.Data.Validators.Helpers;

namespace OrderManagement.Data.Validators.Orders;

public class UpdateOrderLineValidator : AbstractValidator<UpdateOrderLine>
{
    public UpdateOrderLineValidator()
    {
        RuleFor(p => p.LineNumber).GreaterThanOrEqualTo(1)
                                  .WithErrorCode("lineNumber_required")
                                  .WithMessage("Line Number cannot be less than 1");

        RuleFor(p => p.ProductCode).NotEmpty()
                                .WithErrorCode("productCode_required").WithMessage("Product Code cannot be empty")
                                .Must(StringValidators.BeValidInput).WithMessage("Please validate your input");

        RuleFor(x => x.ProductType).IsInEnum()
                                   .WithMessage("ProductType must be a valid value.");

        RuleFor(x => x.CostPrice).NotEmpty()
                        .WithErrorCode("costPrice_required")
                        .WithMessage("CostPrice is required.")
                        .WithMessage("CostPrice must be a decimal with up to 18 digits in total and 2 decimal places.");

        RuleFor(x => x.SalesPrice).NotEmpty()
                        .WithErrorCode("salesPrice_required")
                        .WithMessage("SalesPrice is required.")
                        .WithMessage("SalesPrice must be a decimal with up to 18 digits in total and 2 decimal places.")
                        .GreaterThanOrEqualTo(x => x.CostPrice)
                        .WithMessage("SalesPrice must be greater than or equal to CostPrice.");

        RuleFor(p => p.Quantity).GreaterThanOrEqualTo(1)
                                  .WithErrorCode("quantity_required")
                                  .WithMessage("Quantity cannot be less than 1");
    }
}