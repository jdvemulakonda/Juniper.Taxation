using FluentValidation;

namespace Juniper.Taxation.Core.Application.Models.Validators
{
    public class TaxByLocationQueryValidator:AbstractValidator<TaxByLocationQuery>
    {
        public TaxByLocationQueryValidator()
        {
            RuleFor(p => p).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Request cannot be null");
            RuleFor(p => p.Location).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Request Location cannot be null");

            When(m=>m.Location!=null,()=>
            {
                RuleFor(p => p.Location.Zip).NotNull().NotEmpty().Matches("^[0-9]{5}(?:-[0-9]{4})?$")
                    .WithMessage("Zip code not in valid format");
            });

        }
    }
}