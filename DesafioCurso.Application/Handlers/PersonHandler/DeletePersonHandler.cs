

using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.PersonHandler
{
    public class DeletePersonHandler : IRequestHandler<DeletePesonRequest, DeletePersonResponse>
    {

        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;

        public DeletePersonHandler(IPersonRepository personRepository, IUnitOfWork<ApplicationDbContext> uow)
        {
            _personRepository = personRepository;
            _uow = uow;
        }

        public async Task<DeletePersonResponse> Handle(DeletePesonRequest request, CancellationToken cancellationToken)
        {

            var personId = await _personRepository.GetById(request.Id);

            if (personId is null)
                throw new NotFoundException("Pessoa não encontrada.");

            if (personId.ReleaseSale == true)
                throw new CustomException ("Não foi possível realizar a exclusão da pessoa, desative a opção liberar venda e tente novamente.");

            _personRepository.Delete(personId);
            await _uow.Commit();

            return personId.Adapt<DeletePersonResponse>();

        }
    }
}
