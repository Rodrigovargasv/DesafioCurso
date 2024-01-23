using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UnitHandler
{
    public class UpdateUnitHandler : IRequestHandler<UpdateUnitRequest, UpdateUnitResponse>
    {
        private readonly IUnitRepository _context;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;

        public UpdateUnitHandler(IUnitRepository context, IUnitOfWork<ApplicationDbContext> uow)
        {
            _context = context;
            _uow = uow;
     
        }

        public async Task<UpdateUnitResponse> Handle(UpdateUnitRequest request, CancellationToken cancellationToken)
        {
           
            var unitId = await _context.GetById(request.IdOrIdentifier);

            if (!string.IsNullOrEmpty(request.Decription))
                unitId.Decription = request.Decription;

            _context.Update(unitId);

            await _uow.Commit();

            return unitId.Adapt<UpdateUnitResponse>();
        }
    }
}