using DesafioCurso.Application.Commands.Response;
using MediatR;

namespace DesafioCurso.Application.Commands.Request
{
    public class CreateUnitRequest : IRequest<CreateUnitResponse>
    {
        public string Acronym { get; set; } // Sigla

        public string Decription { get; set; }

    }
}
