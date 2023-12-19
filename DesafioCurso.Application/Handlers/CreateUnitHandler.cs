using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using Mapster;
using MediatR;
using Unit = DesafioCurso.Domain.Entities.Unit;
using FluentValidation;
using DesafioCurso.Domain.Common.Exceptions;



namespace DesafioCurso.Application.Handlers
{
    public class CreateUnitHandler : IRequestHandler<CreateUnitRequest, CreateUnitResponse>
    {

        private readonly IUnitRepository _context;
        private readonly IUnitOfWork _uow;
        private readonly UnitValidation _unitValidation;

        public CreateUnitHandler(IUnitRepository context, IUnitOfWork uow, UnitValidation validations)
        {
            _context = context;
            _uow = uow;
            _unitValidation = validations;
        }



        // Implementando a interface Handle
        public async Task<CreateUnitResponse> Handle(CreateUnitRequest request, CancellationToken cancellationToken)
        {

            var unit = request.Adapt<Unit>();


            var unitValidation = await _unitValidation.ValidateAsync(unit);

            if (!unitValidation.IsValid) throw new ValidationException(unitValidation.Errors);


            var unitExists = await _context.PropertyAcronymExists(unit.Acronym);

            if (unitExists != null)
                throw new CustomException($"Já existe um unidade: {unit.Acronym}");


            await _context.Create(unit);
            await _uow.Commit();

   
            return unit.Adapt<CreateUnitResponse>();

        }
    }
}
