using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.PersonHandler
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdRequest, GetPersonByIdResponse>
    {
        public readonly IPersonRepository _personRepository;

        public GetPersonByIdHandler(IPersonRepository context)
        {
            _personRepository = context;
        }

        public async Task<GetPersonByIdResponse> Handle(GetPersonByIdRequest request, CancellationToken cancellationToken)
        {
            var unitId = await _personRepository.GetById(request.Id);

            if (unitId is null)
                throw new NotFoundException("Pessoa não encontrada.");

            return unitId.Adapt<GetPersonByIdResponse>();
        }
    }
}