using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{
    public class ProductRepository : RepositoryBase<Product, ApplicationDbContext>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Lista todos os produtos vendaveis
        public async Task<IEnumerable<Product>> GetAllProductsSaleables(int page, int pageSize)
        {
            return await _dbContext.Products
                 .Where(s => s.Saleable == true)
                 .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(); 
        }

        // Verifica se código de barras já existe no banco de dados.
        public async Task<Product> VerifyIfBarCodeExists(string? barCode)
        {
            if (barCode == null)
            {
                // Lida com o caso em que o código de barras é nulo
                return null;
            }

            return await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BarCode == barCode);
        }
    }
}