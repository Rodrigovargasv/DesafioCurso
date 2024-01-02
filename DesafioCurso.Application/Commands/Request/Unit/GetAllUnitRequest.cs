using DesafioCurso.Application.Commands.Response.Unit;
using MediatR;


namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class GetAllUnitRequest : IRequest<IEnumerable<GetAllUnitResponse>>
    {
        public int Quantity { get; set; }
    }
}
