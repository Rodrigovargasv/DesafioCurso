using DesafioCurso.Application.Commands.Response.User;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetUserByIdRequest : IRequest<GetUserByIdResponse>
    {
        public string IdOrIdentifier { get; set; }
    }
}