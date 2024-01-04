
using DesafioCurso.Application.Commands.Response.User;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class LoginUserRequest : IRequest<LoginUserResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
