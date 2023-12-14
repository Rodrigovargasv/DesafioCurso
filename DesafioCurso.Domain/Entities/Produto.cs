
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Entities
{
    public class Produto : EntityBase
    {
        public string DescricaoCompleta { get; set; }
        public string DescricaoResumida { get; set; }
        public decimal Preco { get; set; } 
        public int QuantidadeEstoque { get; set;}
        public int CodigoBarras { get; set; }
        public bool Ativo { get; set; }
        public bool Vendavel { get; set; }
        public string SiglaUnidade { get; set; }

        public Unidade UnidadeProduto { get; set;}


    }
}
