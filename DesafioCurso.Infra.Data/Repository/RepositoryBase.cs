
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{
    public class RepositoryBase<TEntity, TDbContext> 
        : IRepositoryBase<TEntity> where TEntity : class
        where TDbContext : DbContext
    {
        // Contexto do banco de dados
        private readonly TDbContext _context;

        public RepositoryBase(TDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TEntity>> GetAll(int quantidade)
        {
            return await _context.Set<TEntity>().Take(quantidade).ToListAsync();
        }

        public async Task Create(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);    
        
        }

        public void Update(TEntity entity)
        {
             _context.Set<TEntity>().Update(entity);
        }

        public async Task<TEntity> GetById(Guid? id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public void Delete(TEntity entity)
        {
             _context.Set<TEntity>().Remove(entity);
        }
    }
}
