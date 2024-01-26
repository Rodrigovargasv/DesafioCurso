using DesafioCurso.Application.Commands.Request.User;
using FluentValidation;

namespace DesafioCurso.Application.Validations.User
{
    public class GetAllUserRequestValidation : AbstractValidator<GetAllUserRequest>
    {
        public GetAllUserRequestValidation()
        {
            RuleFor(request => request.Page).GreaterThan(0).WithMessage("A página deve ser maior que zero.");
            RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("O tamanho da página deve ser maior que zero.");
        }
    }
}