using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Application.Commands.Response.Unit
{
    public class GetAllUnitResponse : EntityBase
    {
        public string Acronym { get; set; } // Sigla
        public string Decription { get; set; }
        public string? Identifier { get; set; }
    }
}