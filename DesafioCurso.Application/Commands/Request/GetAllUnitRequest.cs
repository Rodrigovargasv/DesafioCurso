using DesafioCurso.Application.Commands.Response;
using MediatR;


namespace DesafioCurso.Application.Commands.Request
{
    public class GetAllUnitRequest : IRequest<IEnumerable<GetAllUnitResponse>>
    {
        public int Quantity { get; set; }
    }
}
