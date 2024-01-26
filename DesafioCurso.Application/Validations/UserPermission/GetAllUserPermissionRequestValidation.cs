using DesafioCurso.Application.Commands.Request.UserPermission;
using FluentValidation;

namespace DesafioCurso.Application.Validations.UserPermission
{
    public class GetAllUserPermissionRequestValidation : AbstractValidator<GetAllUserPermissionRequest>
    {
        public GetAllUserPermissionRequestValidation()
        {
            RuleFor(request => request.Page).GreaterThan(0).WithMessage("A página deve ser maior que zero.");
            RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("O tamanho da página deve ser maior que zero.");
        }
    }
}