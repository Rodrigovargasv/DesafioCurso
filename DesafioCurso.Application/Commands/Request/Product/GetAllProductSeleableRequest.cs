using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class GetAllProductSeleableRequest : IRequest<IEnumerable<GetAllProductSeleableResponse>>
    {
        public PaginationParamenters Paramenters { get; set; }
    }
}