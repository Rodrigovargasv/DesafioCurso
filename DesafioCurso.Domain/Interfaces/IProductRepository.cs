using DesafioCurso.Domain.Entities;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsSaleables(int page, int pageSize);
    }
}