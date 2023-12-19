
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request
{
    public class UpdateUnitRequest : EntityBase, IRequest<UpdateUnitResponse>
    {

        public string Decription { get; set; }

    }
}
