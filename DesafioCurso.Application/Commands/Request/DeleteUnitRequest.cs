
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request
{
    public class DeleteUnitRequest : EntityBase ,IRequest<DeleteUnitResponse>
    {
    }
}
