
using DesafioCurso.Application.Commands.Response.User;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetAllUserTypeManagerRequest : IRequest<IEnumerable<GetAllUserTypeManagerResponse>>
    {
         public int Quantity { get; set; }
    }
}
