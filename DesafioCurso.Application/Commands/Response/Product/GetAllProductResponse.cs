
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Application.Commands.Response.Product
{
    public class GetAllProductResponse : EntityBase
    {
        public string FullDescription { get; set; }
        public string BriefDescription { get; set; } // Descrição Resumida
        public decimal Price { get; set; }
        public int? QuantityStock { get; set; }
        public string BarCode { get; set; }
        public bool Active { get; set; }
        public bool Saleable { get; set; } // vendavel
        public string AcronynmUnit { get; set; }
    }
}
