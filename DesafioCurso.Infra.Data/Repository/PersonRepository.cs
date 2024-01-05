using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DesafioCurso.Infra.Data.Repository
{
    public class PersonRepository : RepositoryBase<Person, ApplicationDbContext>, IPersonRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Person> PropertyAlternativeCodeExist(string alternativeCode)
        {
            if(alternativeCode == null)
                return null;


            return await _dbContext.People.AsNoTracking().FirstOrDefaultAsync(p => p.AlternativeCode == alternativeCode);
        }

     

        public async Task<Person> PropertyDocumentExist(string document)
        {
            if (document == null)
                return null;

            return await _dbContext.People.AsNoTracking().FirstOrDefaultAsync(p => p.Document == document);
        }
    }
}
