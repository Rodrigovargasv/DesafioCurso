using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class GetUnitByIdRequest : IRequest<GetUnitByIdResponse>
    {
        public string IdOrIdentifier { get; set; }
    }
}