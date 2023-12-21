using DesafioCurso.Application.Commands.Response.Unit;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class CreateUnitRequest : IRequest<CreateUnitResponse>
    {
        public string Acronym { get; set; } // Sigla

        public string Decription { get; set; }

    }
}
