using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Entities;
using FluentValidation;

namespace DesafioCurso.Domain.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.FullDescription)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo descrição completa não pode conter espaço em branco.")
                .NotNull()
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.BriefDescription)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo descrição resumida não pode conter espaço em branco.")
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.AcronynmUnit)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo sigla da unidade não pode conter espaço em branco.")
                .NotEmpty()
                .NotNull()
                .MaximumLength(10);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}