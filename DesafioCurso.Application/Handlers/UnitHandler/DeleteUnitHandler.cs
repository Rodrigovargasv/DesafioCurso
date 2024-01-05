using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;
using Unit = DesafioCurso.Domain.Entities.Unit;

namespace DesafioCurso.Application.Handlers.UnitHandler
{
    public class DeleteUnitHandler : IRequestHandler<DeleteUnitRequest, DeleteUnitResponse>
    {
        private readonly IUnitRepository _context;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;

        public DeleteUnitHandler(IUnitRepository context, IUnitOfWork<ApplicationDbContext> uow)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<DeleteUnitResponse> Handle(DeleteUnitRequest request, CancellationToken cancellationToken)
        {
            var unit = request.Adapt<Unit>();

            var unitId = await _context.GetById(unit.Id);

            // Verifica se a unidade existe
            if (unitId is null)
                throw new NotFoundException("Unidade não encontrada");

            _context.Delete(unitId);

            await _uow.Commit();

            return request.Adapt<DeleteUnitResponse>();
        }
    }
}