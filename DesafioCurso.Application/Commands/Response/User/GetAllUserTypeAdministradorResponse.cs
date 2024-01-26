namespace DesafioCurso.Application.Commands.Response.User
{
    public record GetAllUserTypeAdministradorResponse(Guid Id, string Identifier, string FullName, string Nickname, string Email, string Cpf_Cnpj);
}