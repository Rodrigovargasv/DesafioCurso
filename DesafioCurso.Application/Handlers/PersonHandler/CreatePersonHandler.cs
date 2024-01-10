using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.PersonHandler
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonRequest, CreatePersonResponse>
    {
        private readonly IPersonRepository _context;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly PersonValidation _personValidation;
        private readonly IShortIdGeneratorService _shortIdGeneratorService;

        public CreatePersonHandler(IPersonRepository context, IUnitOfWork<ApplicationDbContext> uow, PersonValidation validations, IShortIdGeneratorService shortIdGenerator )
        {
            _context = context;
            _uow = uow;
            _personValidation = validations;
            _shortIdGeneratorService = shortIdGenerator;
        }

        public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {

            #region Validações dos dados informados request e verificações da existência de documento e codígo alternativo no banco de dados
            var person = request.Adapt<Person>();
            var personValidation = await _personValidation.ValidateAsync(person);
            var documentExist = await _context.PropertyDocumentExist(person.Document);
            var alternativeCode = await _context.PropertyAlternativeCodeExist(person.AlternativeCode);

        
            if (person.Document != null)
                person.Document = person.Document.Replace(".", "").Replace("-", "").Replace("/", "");

            if (!personValidation.IsValid) throw new ValidationException(personValidation.Errors);

            if (alternativeCode != null || documentExist != null)
                throw new CustomException("Já existe uma pessoa com estes dados de documento ou codigo alternativo");

            #endregion

            // Gerar identificador unico para cada pessoa criada.
            person.Identifier = _shortIdGeneratorService.GenerateShortId();

            await _context.Create(person);

            await _uow.Commit();

            return person.Adapt<CreatePersonResponse>();
        }
    }
}