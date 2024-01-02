
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Entities
{
    public class Unit : EntityBase
    {
        public string Acronym { get; set; } // Sigla

        public string Decription { get; set; }

        // Propriedade de navegação inversa
        public IEnumerable<Product> RelatedProducts{ get; set; } // Produtos Relacionados
    }
}
