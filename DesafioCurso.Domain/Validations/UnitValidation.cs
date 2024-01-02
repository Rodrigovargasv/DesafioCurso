using DesafioCurso.Domain.Entities;
using FluentValidation;


namespace DesafioCurso.Domain.Validations
{
    public class UnitValidation : AbstractValidator<Unit>

    {
        public UnitValidation() 
        {
            RuleFor(u => u.Acronym)
           .NotEmpty()
           .NotNull();

            RuleFor(u => u.Decription)
                .NotEmpty()
                .NotNull();
        }
    }

}
    
    



