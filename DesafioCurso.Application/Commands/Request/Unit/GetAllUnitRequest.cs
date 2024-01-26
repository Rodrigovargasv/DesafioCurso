using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class GetAllUnitRequest : PaginationParamenters, IRequest<IEnumerable<GetAllUnitResponse>>
    {
    }
}