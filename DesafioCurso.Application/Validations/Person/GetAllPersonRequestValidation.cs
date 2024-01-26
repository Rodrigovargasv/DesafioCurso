using DesafioCurso.Application.Commands.Request.Person;
using FluentValidation;

namespace DesafioCurso.Application.Validations.Person
{
    public class GetAllPersonRequestValidation : AbstractValidator<GetAllPersonRequest>
    {
        public GetAllPersonRequestValidation()
        {
            RuleFor(request => request.Page).GreaterThan(0).WithMessage("A página deve ser maior que zero.");
            RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("O tamanho da página deve ser maior que zero.");
        }
    }
}