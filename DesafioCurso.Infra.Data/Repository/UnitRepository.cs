
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{
    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<Unit> PropertyAcronymExists(string acronym)
        {
            return await _dbContext.Set<Unit>().AsNoTracking().FirstOrDefaultAsync(p => p.Acronym == acronym);
        }

    }
}
