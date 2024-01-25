using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetAllUserTypeSellerRequest : IRequest<IEnumerable<GetAllUserTypeSellerResponse>>
    {
        public PaginationParamenters Paramenters { get; set; }
    }
}