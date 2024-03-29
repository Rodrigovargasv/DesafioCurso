﻿using DesafioCurso.Application.Commands.Request.UserPermission;
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
                     true : throw new NotFoundException("Não foi encontrada a permissão de usuário com o id informando.")
                );

            RuleFor(x => x.UserId)

              .NotEmpty();

            RuleFor(x => x.Role)
              .Must(role => (int)role >= 1 && (int)role <= 4)
              .WithMessage("A propriedade Role deve ser um número entre 1 e 4.");
        }
    }
}