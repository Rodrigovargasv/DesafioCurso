

namespace DesafioCurso.Application.Commands.Response.Product;

public record GetAllProductSeleableResponse(Guid Id, string Identifier,
    string FullDescription, string BriefDescription, decimal Price,
    int QuantityStock, string BarCode, bool Active, bool Saleable, string AcronynmUnit);
