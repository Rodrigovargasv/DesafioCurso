using DesafioCurso.Application.Commands.Response.Product;
using MediatR;
using Newtonsoft.Json;
using System.ComponentModel;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class UpdateProductRequest : IRequest<UpdateProductResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string? FullDescription { get; set; }
        public string? BriefDescription { get; set; } // Descrição Resumida
        public decimal? Price { get; set; }
        public int? QuantityStock { get; set; }

        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? BarCode { get; set; }

        public bool? Active { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Saleable { get; set; } // vendavel

        public string? AcronynmUnit { get; set; }
    }
}