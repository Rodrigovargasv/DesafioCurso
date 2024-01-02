
using DesafioCurso.Domain.Entities;
using FluentValidation;

namespace DesafioCurso.Domain.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    { 
        public ProductValidation() 
        {
            RuleFor(x => x.FullDescription)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.BriefDescription)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.AcronynmUnit)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);


        }
    }
}
