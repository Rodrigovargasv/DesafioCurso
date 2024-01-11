using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class GetProductByIdRequest : IRequest<GetProductByIdResponse>
    {
        public Guid Id { get; set; }
    }
}