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
            #region Verifica se unidade existe no banco de dados, e valida dados informados no request, para a atualização da unidade.

            var unitId = await _context.GetById(request.Id);
            var unitValidation = await _unitValidation.ValidateAsync(unitId);

            if (unitId is null)
                throw new NotFoundException("Unidade não encontrada");
            if (!unitValidation.IsValid)
                throw new ValidationException(unitValidation.Errors);

            #endregion Verifica se unidade existe no banco de dados, e valida dados informados no request, para a atualização da unidade.

            // Recebe os dados do request se tudo estiver correto.
            unitId.Decription = request.Decription;

            _context.Update(unitId);

            await _uow.Commit();

            return unitId.Adapt<UpdateUnitResponse>();
        }
    }
}