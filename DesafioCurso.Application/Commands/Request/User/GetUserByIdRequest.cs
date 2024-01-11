using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetUserByIdRequest :  IRequest<GetUserByIdResponse>
    {
        public Guid Id { get; set; }
    }
}