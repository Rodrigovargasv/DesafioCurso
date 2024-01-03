using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Entities;
using FluentValidation;


namespace DesafioCurso.Domain.Validations
{
    public class UnitValidation : AbstractValidator<Unit>

    {
        public UnitValidation() 
        {
            RuleFor(u => u.Acronym)
           .Must(value => !Utils.ContainsWhitespace(value)).WithMessage("O campo senha não pode conter espaço em branco.")
           .NotEmpty()
           .NotNull();

            RuleFor(u => u.Decription)
                .Must(value => !Utils.ContainsWhitespace(value)).WithMessage("O campo senha não pode conter espaço em branco.")
                .NotEmpty()
                .NotNull();
        }
    }

}
    
    



