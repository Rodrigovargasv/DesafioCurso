
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Entities
{
    public class Person : EntityBase
    {
        public string? FullName { get; set; }
        public string? Document { get; set; }
        public string? City { get; set; }
        public string? Observation { get; set; }
        public string? AlternativeCode { get; set; }
        public bool? ReleaseSale { get; set; }// liberar venda
        public bool? Active { get; set; } 
        
    }
}
