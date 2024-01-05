using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCurso.Domain.Validations
{
    public class UserPermissionValidation : AbstractValidator<UserPermission>
    {
        public UserPermissionValidation() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty();


            //RuleFor(userRole => userRole)
            //    .Must(role => role.Role == UserRole.commonUser || role.Role == UserRole.seller
            //    || role.Role == UserRole.manager
            //    || role.Role == UserRole.administrator)
            //    .WithMessage("O perfil do usuario informado não existe.");
        }
    }
}
