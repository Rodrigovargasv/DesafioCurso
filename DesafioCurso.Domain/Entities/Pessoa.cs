
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Entities
{
    public class Pessoa : EntityBase
    {
        public string NomeCompleto {get; set;}
        public string Documento { get; set; }
        public string Cidade { get; set; }
        public string Observacao { get; set; }
        public string CodigoAlternativo { get; set; }
        public bool LiberaVenda { get; set; }
        public bool Ativo { get; set; }
    }
}
