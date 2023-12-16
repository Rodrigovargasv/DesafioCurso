using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Repository;
using Mapster;
using MediatR;
using Unit = DesafioCurso.Domain.Entities.Unit;

namespace DesafioCurso.Application.Handlers
{
    public class CreateUnitHandler : IRequestHandler<CreateUnitRequest, CreateUnitResponse>
    {

        private readonly UnitRepository _context;
        private readonly IUnitOfWork _uow;

        public CreateUnitHandler(UnitRepository context, IUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }



        // Implementando a interface Handle
        public async Task<CreateUnitResponse> Handle(CreateUnitRequest request, CancellationToken cancellationToken)
        {

            var unit = request.Adapt<Unit>();
       
            await _context.Create(unit);
            await _uow.Commit();

   

            return unit.Adapt<CreateUnitResponse>();

        }
    }
}
