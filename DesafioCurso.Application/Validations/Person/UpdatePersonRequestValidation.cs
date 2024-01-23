using DesafioCurso.Application.Commands.Request.Person;
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
                         throw new NotFoundException("Pessoa não encontrada.");
                     
                     return true; // A validação passou
                 });

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
                       
                    return UtilsValidations.ValidationCpfAndCnpj(value.Replace(".", "").Replace("-", "").Replace("/", ""));
                    
                })
                .WithMessage("CPF ou CNPJ inválido")
                .MustAsync(async (request, cancellationToken) =>
                {

                    if (string.IsNullOrEmpty(request))
                        return true;

                    var personDocument = await _dbContext.People.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Document == request.Replace(".", "").Replace("-", "").Replace("/", ""));

                    if (personDocument != null)
                        throw new BadRequestException("CPF ou CNPJ indisponível."); 
                    

                    return true;
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
                     

                     var personAlternativeCode =  await _dbContext.People.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.AlternativeCode == request) ;

                     if (personAlternativeCode != null)
                         throw new BadRequestException("Já existe um código alternativo com estas informações");


                     return true;
                 });
        }
    }
}
