
using DesafioCurso.Application.Commands.Response.Product;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class GetAllProductRequest : IRequest<IEnumerable<GetAllProductResponse>>
    {
        public int Quantity { get; set; }
    }
}
