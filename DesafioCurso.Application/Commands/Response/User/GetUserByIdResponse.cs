using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Application.Commands.Response.User
{
    public class GetUserByIdResponse : EntityBase
    {
        public string FullName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        // Representa um CPF ou CNPJ.
        public string Cpf_Cnpj { get; set; }
    }
}