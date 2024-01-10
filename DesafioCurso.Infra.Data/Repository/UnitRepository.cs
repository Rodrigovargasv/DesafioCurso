using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{
    public class UnitRepository : RepositoryBase<Unit, ApplicationDbContext>, IUnitRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Verifica se a sigla já existe no banco de dados
        public async Task<Unit> PropertyAcronymExists(string acronym)
        {
            return await _dbContext.Units.AsNoTracking().FirstOrDefaultAsync(p => p.Acronym == acronym);
        }
    }
}