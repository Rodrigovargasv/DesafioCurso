﻿using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Application.Validations.Person
{
    public class UpdatePersonRequestValidation : AbstractValidator<UpdatePersonRequest>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPersonRepository _personRepository;

        public UpdatePersonRequestValidation(ApplicationDbContext dbContext, IPersonRepository personRepository)
        {
            _dbContext = dbContext;
            _personRepository = personRepository;

            RuleFor(x => x.IdOrIdentifier)
                 .MustAsync(async (request, cancellationToken) =>
                 {
                     var personIdOrIdentifier = await _personRepository.GetById(request);

                     if (personIdOrIdentifier == null)
                         throw new NotFoundException("Não foi encontrado a pessoa com o id ou shortId informando.");

                     return true; // A validação passou
                 });

            RuleFor(p => p.FullName)
              .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo descrição completa não pode conter espaço em branco.")
              .MaximumLength(100);

            RuleFor(p => p.Document)
                .Must(value => !UtilsValidations.ContainsWhitespace(value))
                .WithMessage("O campo documento não pode conter espaço em branco.")
                .Must(value =>
                {
                    if (string.IsNullOrEmpty(value))
                        return true;

                    return UtilsValidations.ValidationCpfAndCnpj(value)
                    ? true : throw new BadRequestException("CPF ou CNPJ inválido.");
                })
                .WithMessage("CPF ou CNPJ inválido")
                .MustAsync(async (request, cancellationToken) =>
                {
                    if (string.IsNullOrEmpty(request))
                        return true;

                    return await _dbContext.People.AsNoTracking()
                        .AnyAsync(x => x.Document == request.Replace(".", "").Replace("-", "").Replace("/", ""))
                           ? throw new BadRequestException("CPF ou CNPJ indisponível.") : true;
                });

            RuleFor(p => p.City)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo cidade não pode conter espaço em branco.")
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

                     return await _dbContext.People.AsNoTracking()
                        .AnyAsync(x => x.AlternativeCode == request)
                            ? throw new BadRequestException("Já existe um código alternativo com estas informações") : true;
                 });
        }
    }
}