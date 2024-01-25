using DesafioCurso.Domain.Enums;

namespace DesafioCurso.Application.Commands.Response.UserPermission;

public record GetAllUserPermissionResponse(Guid Id, string Identifier, UserRole Role, Guid UserId);


    