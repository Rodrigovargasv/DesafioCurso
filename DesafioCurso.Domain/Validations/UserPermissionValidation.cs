using DesafioCurso.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
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
                
        }
    }
}
