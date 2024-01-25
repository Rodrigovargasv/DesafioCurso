using DesafioCurso.Domain.Interfaces;
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

        #region Métodos básicos e genéricos de CREATE, READ,UPADATE e DELETE.

        public async Task<IEnumerable<TEntity>> GetAll(int page, int pageSize)
        {
            return await _context.Set<TEntity>()
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<TEntity> GetById(string value)
        {
            var entityQuery = _context.Set<TEntity>().AsNoTracking();

            if (Guid.TryParse(value, out Guid guidValue))
                return await entityQuery.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == guidValue);

            return await entityQuery.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<string>(e, "Identifier") == value);
            
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        #endregion Métodos básicos e genéricos de CREATE, READ,UPADATE e DELETE.
    }
}