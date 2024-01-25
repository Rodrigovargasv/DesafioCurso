using DesafioCurso.Application.Commands.Response.Product;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class DeleteProductRequest : IRequest<DeleteProductResponse>
    {
        public string IdOrIdentifier { get; set; }
    }
}