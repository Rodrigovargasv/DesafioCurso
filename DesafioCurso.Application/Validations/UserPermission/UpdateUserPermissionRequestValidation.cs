using DesafioCurso.Application.Commands.Request.UserPermission;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Application.Validations.UserPermission
{
    public class UpdateUserPermissionRequestValidation : AbstractValidator<UpdateUserPermissionRequest>
    {
        private readonly SqliteDbcontext _dbContext;

        public UpdateUserPermissionRequestValidation(SqliteDbcontext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.UserId)
                 .MustAsync(async (request, cancellationToken) =>
                    await _dbContext.Permissions.AsNoTracking().AnyAsync(x => x.UserId == request) ?
                     true : throw new NotFoundException("Usuário não encontrado") 

                );  

            RuleFor(x => x.UserId)
              .NotNull()
              .NotEmpty();

            RuleFor(x => x.Role)
               .NotNull()
              .NotEmpty()
              .Must(role => (int)role >= 1 && (int)role <= 4)
              .WithMessage("A propriedade Role deve ser um número entre 1 e 4.");




        }
    }
    
}
