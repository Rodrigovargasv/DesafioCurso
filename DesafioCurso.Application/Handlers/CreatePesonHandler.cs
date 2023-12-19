using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers
{
    public class CreatePesonHandler : IRequestHandler<CreatePersonRequest, CreatePersonResponse>
    {
        private readonly IPersonRepository _context;
        private readonly IUnitOfWork _uow;
        private readonly PersonValidation _personValidation;

        public CreatePesonHandler(IPersonRepository context, IUnitOfWork uow, PersonValidation validations)
        {
            _context = context;
            _uow = uow;
            _personValidation = validations;
        }

        public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {

            var person = request.Adapt<Person>();


            var personValidation = await _personValidation.ValidateAsync(person);

            if (!personValidation.IsValid) throw new ValidationException(personValidation.Errors);

            var personExists = await _context.PropertyDocumentAndAlternativeCodeExist(person.Document, person.AlternativeCode);

            if (personExists != null) throw new CustomException("Já existe uma pessoa com estes dados de documento ou codigo alternativo");


            await _context.Create(person);

            await _uow.Commit();

        
            return person.Adapt<CreatePersonResponse>();
        }
    }
}
