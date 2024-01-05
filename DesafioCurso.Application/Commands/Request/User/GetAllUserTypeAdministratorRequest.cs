using DesafioCurso.Application.Commands.Response.User;
using MediatR;


namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetAllUserTypeAdministratorRequest : IRequest<IEnumerable<GetAllUserTypeAdministradorResponse>>
    {
        public int Quantity { get; set; }
    }
}
