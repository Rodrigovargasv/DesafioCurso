
using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers
{

    public class GetAllUnitHandler : IRequestHandler<GetAllUnitRequest, IEnumerable<GetAllUnitResponse>>
    {
        private readonly IUnitRepository _context;
        private readonly UnitValidation _unitValidation;

        public GetAllUnitHandler(IUnitRepository context, UnitValidation unitValidation)
        {
            _context = context;
        
            _unitValidation = unitValidation;
        }

        public async Task<IEnumerable<GetAllUnitResponse>> Handle(GetAllUnitRequest request, CancellationToken cancellationToken)
        {
            
            var units = await _context.GetAll(request.Quantity);

            var unitResponses = units.Adapt<IEnumerable<GetAllUnitResponse>>();


            return unitResponses;
        }
    }
}
