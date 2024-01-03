
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetAllUserRequest :IRequest<IEnumerable<GetAllUserResponse>>
    {
       public int Quantity { get; set; }
    }
}
