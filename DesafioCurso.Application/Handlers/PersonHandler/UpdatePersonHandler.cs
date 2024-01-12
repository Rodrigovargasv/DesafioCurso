using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.PersonHandler
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonRequest, UpdatePersonResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly PersonValidation _personValidation;

        public UpdatePersonHandler(IPersonRepository personRepository, IUnitOfWork<ApplicationDbContext> uow, PersonValidation validation)
        {
            _personRepository = personRepository;
            _uow = uow;
            _personValidation = validation;
        }

        public async Task<UpdatePersonResponse> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            var personId = await _personRepository.GetById(request.IdOrIdentifier);

            // Verifica se a pessoa existe
            if (personId is null)
                throw new NotFoundException("Pessoa não encontrado");

            #region Atualiza as propriedades da unidade com os dados da requisição

            if (!string.IsNullOrEmpty(request.FullName))
                personId.FullName = request.FullName;

            if (!string.IsNullOrEmpty(request.Document))
            {
                personId.Document = request.Document.Replace(".", "").Replace("-", "").Replace("/", "");

                // Valida se o documento já existe no banco de dados.
                var documentExist = await _personRepository.PropertyDocumentExist(personId.Document);

                if (documentExist != null)
                    throw new CustomException("O documento não pode ser cadastrado, tente novamente");
            }

            if (!string.IsNullOrEmpty(request.City))
                personId.City = request.City.ToUpper() ;

            if (!string.IsNullOrEmpty(request.Observation))
                personId.Observation = request.Observation;

            if (!string.IsNullOrEmpty(request.AlternativeCode))
            {
                personId.AlternativeCode = request.AlternativeCode;

                // Valida se o código alternativo já existe no banco de dados
                var alternativeCode = await _personRepository.PropertyAlternativeCodeExist(personId.AlternativeCode);

                if (alternativeCode != null)
                    throw new CustomException("Já existe um código alternativo com estas informações, tente novamente");
            }

            if (!string.IsNullOrEmpty(request.ReleaseSale.ToString()))
                personId.ReleaseSale = request.ReleaseSale;

            if (!string.IsNullOrEmpty(request.Active.ToString()))
                personId.Active = request.Active;

            #endregion Atualiza as propriedades da unidade com os dados da requisição

            var personValidation = await _personValidation.ValidateAsync(personId);

            // Se os dados informado no requesto para pessoa não for válido, é lançado uma exceção.
            if (!personValidation.IsValid)
                throw new ValidationException(personValidation.Errors);

            _personRepository.Update(personId);
            await _uow.Commit();

            return personId.Adapt<UpdatePersonResponse>();
        }
    }
}