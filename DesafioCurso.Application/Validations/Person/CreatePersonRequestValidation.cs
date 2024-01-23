using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Commons;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Application.Validations.Person
{
    public class CreatePersonRequestValidation : AbstractValidator<CreatePersonRequest>
    {
        private readonly ApplicationDbContext _context;

        public CreatePersonRequestValidation(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.FullName)
               .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo descrição completa não pode conter espaço em branco.")
               .NotNull()
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(p => p.Document)
                .Must(value => !UtilsValidations.ContainsWhitespace(value))
                .WithMessage("O campo documento não pode conter espaço em branco.")
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

                    return await _context.People.AsNoTracking()
                        .AnyAsync(x => x.Document == request) 
                            ? throw new BadRequestException("CPF ou CNPJ indisponível.") : true;
                });

            RuleFor(p => p.City)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo cidade não pode conter espaço em branco.")
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);

            RuleFor(p => p.Observation)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo obeservação não pode conter espaço em branco.")
                .MaximumLength(250);

            RuleFor(p => p.AlternativeCode)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo codigo alternativo não pode conter espaço em branco.")
                .MaximumLength(50)
                 .MustAsync(async (request, cancellationToken) =>
                 {
                     if (string.IsNullOrWhiteSpace(request))
                         return true;

                     return await _context.People.AsNoTracking()
                         .AnyAsync(x => x.AlternativeCode == request)
                            ? throw new BadRequestException("Código alternativo indisponível.") : true;
                 });

        }
    }
}
