using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class GetAllUnitRequest : IRequest<IEnumerable<GetAllUnitResponse>>
    {
        public PaginationParamenters Paramenters { get; set; }
    }
}