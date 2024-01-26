using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Commons;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Application.Validations.User
{
    public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
    {
        private readonly SqliteDbcontext _dbContext;
        private readonly ApplicationDbContext _context;

        public CreateUserRequestValidation(SqliteDbcontext dbContext, ApplicationDbContext context)
        {
            _dbContext = dbContext;
            _context = context;

            RuleFor(x => x.FullName)
               .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo nome não pode conter espaço em branco.")
               .NotNull()
               .MaximumLength(100);

            RuleFor(x => x.Nickname)
                 .Must(value => !UtilsValidations.ContainsWhitespace(value))
                 .WithMessage("O campo apelido não pode conter espaço em branco.")
                   .MustAsync(async (request, cancellationToken) =>

                    await _dbContext.Users.AsNoTracking().AnyAsync(x => x.Nickname == request)
                        ? throw new BadRequestException("O apelido está indisponível") : true
                );

            RuleFor(x => x.Email)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo email não pode conter espaço em branco.")
                .EmailAddress()
                .MustAsync(async (request, cancellationToken) =>

                    await _dbContext.Users.AsNoTracking().AnyAsync(x => x.Email == request)
                        ? throw new BadRequestException("O email está indisponível") : true
                );

            RuleFor(x => x.Password)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo senha não pode conter espaço em branco.")
                .NotNull()
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$!%^&*])[A-Za-z\d@#$!%^&*]{8,}$")
                .WithMessage("A senha deve conter no mínimo 1 letra maiúscula, 1 letra minúsculas, 1 caracter especial e 8 caracteres.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .NotNull()
                .Equal(p => p.Password)
                .WithMessage("A senha e confirmação de senha devem ser iguais.");

            RuleFor(p => p.Cpf_Cnpj)
               .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo CPF/CNPJ não pode conter espaço em branco.")
               .Must(value =>
               {
                   if (string.IsNullOrEmpty(value))
                       return true;

                   return UtilsValidations.ValidationCpfAndCnpj(value) ?
                       true : throw new BadRequestException("CPF ou CNPJ Inválido.");
               })
                .MustAsync(async (request, cancellationToken) =>
                {
                    if (string.IsNullOrWhiteSpace(request))
                        return true;

                    var isCpfCnpjUnavailable = await _dbContext.Users.AsNoTracking()
                        .AnyAsync(x => x.Cpf_Cnpj == request.Replace(".", "").Replace("-", "").Replace("/", "")) ||

                        await _context.People.AsNoTracking()
                            .AnyAsync(x => x.Document == request.Replace(".", "").Replace("-", "").Replace("/", ""));

                    return isCpfCnpjUnavailable ? throw new BadRequestException("CPF ou CNPJ indisponível.") : true;
                });
        }
    }
}