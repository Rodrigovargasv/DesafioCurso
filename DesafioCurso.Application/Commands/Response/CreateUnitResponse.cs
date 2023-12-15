
using DesafioCurso.Domain.Commons;

namespace DesafioCurso.Domain.Commands.Response
{
    public class CreateUnitResponse : EntityBase
    {
        public string Acronym { get; set; } // Sigla

        public string Decription { get; set; }

    }
}
