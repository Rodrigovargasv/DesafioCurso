using DesafioCurso.Application.Commands.Response.Person;
using MediatR;
using Newtonsoft.Json;
using System.ComponentModel;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace DesafioCurso.Application.Commands.Request.Person
{
    public class UpdatePersonRequest : IRequest<UpdatePersonResponse>
    {
        [JsonIgnore]
        public string IdOrIdentifier { get; set; }

        public string? FullName { get; set; }
        public string? Document { get; set; }
        public string? City { get; set; }
        public string? Observation { get; set; }
        public string? AlternativeCode { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? ReleaseSale { get; set; } // liberar venda

        public bool? Active { get; set; }
    }
}