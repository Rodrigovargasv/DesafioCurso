using DesafioCurso.Application.Commands.Response.Unit;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class DeleteUnitRequest : IRequest<DeleteUnitResponse>
    {
        public string IdOrIdentifier { get; set; }
    }
}