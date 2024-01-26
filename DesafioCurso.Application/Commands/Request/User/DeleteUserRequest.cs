using DesafioCurso.Application.Commands.Response.User;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class DeleteUserRequest : IRequest<DeleteUserResponse>
    {
        public string IdOrIdentifier { get; set; }
    }
}