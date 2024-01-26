using DesafioCurso.Application.Commands.Request.Unit;
using FluentValidation;

namespace DesafioCurso.Application.Validations.Unit
{
    public class GetAllUnitRequestValidation : AbstractValidator<GetAllUnitRequest>
    {
        public GetAllUnitRequestValidation()
        {
            RuleFor(request => request.Page).GreaterThan(0).WithMessage("A página deve ser maior que zero.");
            RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("O tamanho da página deve ser maior que zero.");
        }
    }
}