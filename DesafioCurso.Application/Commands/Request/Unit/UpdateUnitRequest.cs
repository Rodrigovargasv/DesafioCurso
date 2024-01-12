using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Commons;
using MediatR;
using System.Text.Json.Serialization;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class UpdateUnitRequest : IRequest<UpdateUnitResponse>
    {
        [JsonIgnore]
        public string IdOrIdentifier { get; set; }

        public string? Decription { get; set; }
    }
}