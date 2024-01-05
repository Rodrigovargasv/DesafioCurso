using DesafioCurso.Application.Commands.Response.UserPermission;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.UserPermission
{
    public class GetAllUserPermissionRequest : IRequest<IEnumerable<GetAllUserPermissionResponse>>
    {
        public int Quantity { get; set; }
    }
}