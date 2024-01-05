using DesafioCurso.Application.Commands.Response.User;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetAllUserTypeSellerRequest : IRequest<IEnumerable<GetAllUserTypeSellerResponse>>
    {
        public int Quantity { get; set; }
    }
}
