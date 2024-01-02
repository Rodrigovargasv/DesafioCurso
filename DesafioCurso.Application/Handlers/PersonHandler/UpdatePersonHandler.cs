using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.PersonHandler
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonRequest, UpdatePersonResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _uow;
        private readonly PersonValidation _personValidation;

        public UpdatePersonHandler(IPersonRepository personRepository, IUnitOfWork uow, PersonValidation validation)
        {
            _personRepository = personRepository;
            _uow = uow;
            _personValidation = validation;
        }

        public async Task<UpdatePersonResponse> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            var personId = await _personRepository.GetById(request.Id);

            // Verifica se a unidade existe
            if (personId is null)
                throw new NotFoundException("Pessoa não encontrada");


            #region Atualiza as propriedades da unidade com os dados da requisição

            if (!string.IsNullOrEmpty(request.FullName))
                personId.FullName = request.FullName;

            if (!string.IsNullOrEmpty(request.Document))
                personId.Document = request.Document;

            if (!string.IsNullOrEmpty(request.City))
                personId.City = request.City;

            if (!string.IsNullOrEmpty(request.Observation))
                personId.Observation = request.Observation;

            if (!string.IsNullOrEmpty(request.AlternativeCode))
                personId.AlternativeCode = request.AlternativeCode;

            personId.ReleaseSale = string.IsNullOrEmpty(request.ReleaseSale.ToString()) ? personId.ReleaseSale : request.ReleaseSale;
            personId.Active = string.IsNullOrEmpty(request.Active.ToString()) ? personId.Active : request.Active;
            #endregion


            // Verifica se documento ou codigo alternativo já existe no banco de dados.
            var personExist = await _personRepository.PropertyDocumentAndAlternativeCodeExist(personId.Document, personId.AlternativeCode);

            if (personExist != null)
                throw new CustomException("Já existe uma pessoa com estes dados de documento ou codigo alternativo");

            var personValidation = await _personValidation.ValidateAsync(personId);

            // Se a unidade não for válida, lança uma exceção de validação
            if (!personValidation.IsValid)
                    throw new ValidationException(personValidation.Errors);

            // Atualiza a entidade no contexto
            _personRepository.Update(personId);

            // Commit das alterações no banco de dados
            await _uow.Commit();


            return personId.Adapt<UpdatePersonResponse>();
        }
    }
}