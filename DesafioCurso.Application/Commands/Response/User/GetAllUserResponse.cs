namespace DesafioCurso.Application.Commands.Response.User
{
    public record GetAllUserResponse(Guid Id, string Identifier, string FullName, string Nickname, string Email, string Cpf_Cnpj);
}