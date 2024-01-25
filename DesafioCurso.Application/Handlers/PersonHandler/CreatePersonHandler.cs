using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.PersonHandler
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonRequest, CreatePersonResponse>
    {
        private readonly IPersonRepository _context;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly IShortIdGeneratorService _shortIdGeneratorService;

        public CreatePersonHandler(IPersonRepository context, IUnitOfWork<ApplicationDbContext> uow, IShortIdGeneratorService shortIdGenerator)
        {
            _context = context;
            _uow = uow;
            _shortIdGeneratorService = shortIdGenerator;
        }

        public async Task<CreatePersonResponse> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = request.Adapt<Person>();

            if (person.Document != null)
                person.Document = person.Document.Replace(".", "").Replace("-", "").Replace("/", "");

            person.City = person.City.ToUpper();

            // Gerar identificador unico para cada pessoa criada.
            person.Identifier = _shortIdGeneratorService.GenerateShortId();

            await _context.Create(person);

            await _uow.Commit();

            return person.Adapt<CreatePersonResponse>();
        }
    }
}