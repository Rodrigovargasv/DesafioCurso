using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Application.Commands.Response.Person
{
    public class GetPersonByIdResponse : EntityBase
    {
        public string FullName { get; set; }
        public string Surnamed { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Representa um CPF ou CNPJ.
        public string Cpf_Cnpj { get; set; }
    }
}