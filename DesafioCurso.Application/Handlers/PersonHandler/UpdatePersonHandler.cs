using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.PersonHandler
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonRequest, UpdatePersonResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;

        public UpdatePersonHandler(IPersonRepository personRepository, IUnitOfWork<ApplicationDbContext> uow)
        {
            _personRepository = personRepository;
            _uow = uow;
        }

        public async Task<UpdatePersonResponse> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            var personId = await _personRepository.GetById(request.IdOrIdentifier);

            if (!string.IsNullOrEmpty(request.FullName))
                personId.FullName = request.FullName;

            if (!string.IsNullOrEmpty(request.Document))
                personId.Document = request.Document.Replace(".", "").Replace("-", "").Replace("/", "");

            if (!string.IsNullOrEmpty(request.City))
                personId.City = request.City.ToUpper();

            if (!string.IsNullOrEmpty(request.Observation))
                personId.Observation = request.Observation;

            if (!string.IsNullOrEmpty(request.AlternativeCode))
                personId.AlternativeCode = request.AlternativeCode;

            if (!string.IsNullOrEmpty(request.ReleaseSale.ToString()))
                personId.ReleaseSale = request.ReleaseSale;

            if (!string.IsNullOrEmpty(request.Active.ToString()))
                personId.Active = request.Active;

            _personRepository.Update(personId);

            await _uow.Commit();

            return personId.Adapt<UpdatePersonResponse>();
        }
    }
}