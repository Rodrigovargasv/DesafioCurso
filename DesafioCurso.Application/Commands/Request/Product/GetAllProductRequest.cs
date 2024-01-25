using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class GetAllProductRequest : IRequest<IEnumerable<GetAllProductResponse>>
    {
        public PaginationParamenters Paramenters { get; set; }
    }
}