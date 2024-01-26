using DesafioCurso.Application.Commands.Response.Product;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class GetProductByIdRequest : IRequest<GetProductByIdResponse>
    {
        public string IdOrIdentifier { get; set; }
    }
}