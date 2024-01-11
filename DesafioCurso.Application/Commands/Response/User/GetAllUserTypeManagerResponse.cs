using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Application.Commands.Response.User
{
    public class GetAllUserTypeManagerResponse : EntityBase
    {
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

        // Representa um CPF ou CNPJ.
        public string Cpf_Cnpj { get; set; }
    }
}