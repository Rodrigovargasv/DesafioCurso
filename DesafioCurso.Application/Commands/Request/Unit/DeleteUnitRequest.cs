using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class DeleteUnitRequest : IRequest<DeleteUnitResponse>
    {
        public string IdOrIdentifier { get; set; }
    }
}