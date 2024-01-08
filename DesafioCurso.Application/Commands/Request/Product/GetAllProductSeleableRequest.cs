using DesafioCurso.Application.Commands.Response.Product;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class GetAllProductSeleableRequest : IRequest<IEnumerable<GetAllProductSeleableResponse>>
    {
        public int Quantity { get; set; }
    }
}