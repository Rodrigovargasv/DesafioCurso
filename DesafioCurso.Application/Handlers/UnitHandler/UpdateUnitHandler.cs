using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UnitHandler
{
    public class UpdateUnitHandler : IRequestHandler<UpdateUnitRequest, UpdateUnitResponse>
    {
        private readonly IUnitRepository _context;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly UnitValidation _unitValidation;

        public UpdateUnitHandler(IUnitRepository context, IUnitOfWork<ApplicationDbContext> uow, UnitValidation unitValidation)
        {
            _context = context;
            _uow = uow;
            _unitValidation = unitValidation;
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