using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{
    public class PersonRepository : RepositoryBase<Person, ApplicationDbContext>, IPersonRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Verifica se o código altenativo ja este no banco de dados
        public async Task<Person> PropertyAlternativeCodeExist(string alternativeCode)
        {
            if (alternativeCode == null)
                return null;

            return await _dbContext.People.AsNoTracking().FirstOrDefaultAsync(p => p.AlternativeCode == alternativeCode);
        }

        // Verifica se o documento ja este no banco de dados
        public async Task<Person> PropertyDocumentExist(string document)
        {
            if (document == null)
                return null;

            return await _dbContext.People.AsNoTracking().FirstOrDefaultAsync(p => p.Document == document);
        }
    }
}