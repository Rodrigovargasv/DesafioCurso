
using DesafioCurso.Application.Commands.Response.User;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
        public string FullName { get; set; }
        public string? Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Representa um CPF ou CNPJ.
        public string? Cpf_Cnpj { get; set; }
    }
}
