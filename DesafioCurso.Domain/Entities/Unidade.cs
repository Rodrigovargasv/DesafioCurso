
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Entities
{
    public class Unidade : EntityBase
    {
        public string Sigla { get; set; }

        public string Descricao { get; set; }

        // Propriedade de navegação inversa
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
