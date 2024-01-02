
using DesafioCurso.Domain.Entities;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product> VerifyIfBarCodeExists(string? barCode);
    }
}
