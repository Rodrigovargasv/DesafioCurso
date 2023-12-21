using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.PersonHandler
{
    public class GetAllPersonHandler : IRequestHandler<GetAllPersonRequest, IEnumerable<GetAllPersonResponse>>
    {
        private readonly IPersonRepository _context;

        public GetAllPersonHandler(IPersonRepository personRepository)
        {
            _context = personRepository;
        }

        public async Task<IEnumerable<GetAllPersonResponse>> Handle(GetAllPersonRequest request, CancellationToken cancellationToken)
        {
            var units = await _context.GetAll(request.Quantity);

            var unitResponses = units.Adapt<IEnumerable<GetAllPersonResponse>>();


            return unitResponses;

        }
    }
}
