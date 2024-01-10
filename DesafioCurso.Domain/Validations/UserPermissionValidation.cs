using DesafioCurso.Domain.Entities;
using FluentValidation;

namespace DesafioCurso.Domain.Validations
{
    public class UserPermissionValidation : AbstractValidator<UserPermission>
    {
        public UserPermissionValidation()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty();
        }
    }
}