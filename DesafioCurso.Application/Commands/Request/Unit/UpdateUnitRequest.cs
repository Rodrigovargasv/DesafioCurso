using DesafioCurso.Application.Commands.Response.Unit;
using MediatR;
using System.Text.Json.Serialization;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class UpdateUnitRequest : IRequest<UpdateUnitResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string? Decription { get; set; }
    }
}