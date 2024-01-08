using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Application.Commands.Response.Unit
{
    public class GetUnitByIdResponse : EntityBase
    {
        public string Acronym { get; set; } // Sigla

        public string Decription { get; set; }
    }
}