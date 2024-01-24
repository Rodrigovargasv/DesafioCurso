

using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Application.Validations.Login
{
    public class LoginRequestValidation : AbstractValidator<LoginUserRequest>
    {

        private readonly SqliteDbcontext _sqliteDbcontext;
        private readonly IPasswordManger _passwordManger;

        public LoginRequestValidation(SqliteDbcontext sqliteDbcontext, IPasswordManger passwordManger)
        {
            _sqliteDbcontext = sqliteDbcontext;
            _passwordManger = passwordManger;

            var userName = new Domain.Entities.User();

            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull()
                .MustAsync(async (request, cancellationToken) =>
                {
                    var userDate = await _sqliteDbcontext.Users
                        .AsNoTracking().AnyAsync(x => x.Nickname == request || x.Email == request);

                    userName = await _sqliteDbcontext.Users
                        .AsNoTracking().FirstOrDefaultAsync(x => x.Nickname == request || x.Email == request);


                    return userDate ? true : throw new NotFoundException("Usuário ou senha inválido");
                });

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                 .MustAsync(async (request, cancellationToken) =>
                 {
                    var verifyPassword = _passwordManger.VerifyPassword(userName.Password, request);

                     if (verifyPassword == false)
                         throw new BadRequestException("Usuário ou senha inválido.");

                     return true;

                 });
        }
    }
}
