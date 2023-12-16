
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        // Contexto do banco de dados
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task Create(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);    
        
        }

        public void Update(TEntity entity)
        {
             _context.Set<TEntity>().Update(entity);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync();
        }

        public void Delete(TEntity entity)
        {
             _context.Set<TEntity>().Remove(entity);
        }
    }
}
