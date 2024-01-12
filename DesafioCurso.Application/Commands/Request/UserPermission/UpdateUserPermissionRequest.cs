using DesafioCurso.Application.Commands.Response.UserPermission;
using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace DesafioCurso.Application.Commands.Request.UserPermission
{
    public class UpdateUserPermissionRequest : IRequest<UpdateUserPermissionResponse>
    {
        public UserRole Role { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}