
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Product> VerifyIfBarCodeExists(string? barCode)
        {
            if (barCode == null)
            {
                // Lida com o caso em que o código de barras é nulo
                return null;
            }

            return await _dbContext.Set<Product>()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BarCode == barCode);
            
        }
    }
    
}
