using DesafioCurso.Application.Commands.Response.UserPermission;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.UserPermission
{
    public class GetAllUserPermissionRequest : IRequest<IEnumerable<GetAllUserPermissionResponse>>
    {
        public PaginationParamenters Paramenters { get; set; }
    }
}