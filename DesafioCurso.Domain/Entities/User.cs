using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Entities
{
    public class User : EntityBase
    {
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Representa um CPF ou CNPJ.
        public string? Cpf_Cnpj { get; set; }

        public UserPermission Permission { get; set; }
    }
}