namespace DesafioCurso.Application.Commands.Response.Product
{
    public record UpdateProductResponse(Guid Id, string Identifier,
        string fullDescription, string briefDescription, decimal price,
        int quantityStock, string barCode, bool active, bool saleable, string acronynmUnit);
}