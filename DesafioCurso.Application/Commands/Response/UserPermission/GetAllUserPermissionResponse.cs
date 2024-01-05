using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Enums;


namespace DesafioCurso.Application.Commands.Response.UserPermission
{
    public class GetAllUserPermissionResponse : EntityBase
    {

        public UserRole Role { get; set; }

        public Guid UserId { get; set; }
    }
}
