using FluentValidation;

namespace Juniper.Taxation.Core.Application.Models.Validators
{
    public class OrderSalesTaxCommandValidator:AbstractValidator<OrderSalesTaxCommand>
    {
        public OrderSalesTaxCommandValidator()
        {
            RuleFor(p => p).NotNull().WithMessage("Request cannot be null");
        }
    }
}