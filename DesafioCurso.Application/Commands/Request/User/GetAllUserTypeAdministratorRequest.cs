using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetAllUserTypeAdministratorRequest : PaginationParamenters, IRequest<IEnumerable<GetAllUserTypeAdministradorResponse>>
    {
    }
}