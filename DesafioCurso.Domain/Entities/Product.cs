
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Entities
{
    public class Product : EntityBase
    {
        public string FullDescription { get; set; }
        public string BriefDescription { get; set; } // Descrição Resumida
        public decimal Price { get; set; } 
        public int QuantityStock { get; set;}
        public int BarCode { get; set; }
        public bool Active { get; set; }
        public bool Saleable { get; set; } // vendavel
        public string AcronynmUit { get; set; } 

        public Unit UnitProduct { get; set;}


    }
}
