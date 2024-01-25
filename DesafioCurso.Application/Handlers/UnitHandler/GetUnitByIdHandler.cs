using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UnitHandler
{
    public class GetUnitByIdHandler : IRequestHandler<GetUnitByIdRequest, GetUnitByIdResponse>
    {
        private readonly IUnitRepository _unitRepository;

        public GetUnitByIdHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<GetUnitByIdResponse> Handle(GetUnitByIdRequest request, CancellationToken cancellationToken)
        {
            var unitId = await _unitRepository.GetById(request.IdOrIdentifier);
            return unitId.Adapt<GetUnitByIdResponse>();
        }
    }
}