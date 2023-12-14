
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string NomeCompleto { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int CpfCnpj { get; set; }
    }
}
