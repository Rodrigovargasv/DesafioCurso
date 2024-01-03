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

        public async Task<Person> PropertyDocumentAndAlternativeCodeExist(string document, string alternativeCode)
        {
            if (document == null || alternativeCode == null)
            
                // Lida com o caso em que o código de barras é nulo
                return null;


            return await _dbContext.Set<Person>().AsNoTracking().FirstOrDefaultAsync(p => p.Document == document || p.AlternativeCode == alternativeCode);
        }
    }
}
