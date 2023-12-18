
using DesafioCurso.Domain.Entities;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        Task<Person> PropertyDocumentAndAlternativeCodeExist(string document, string AlternativeCode);    
    }
}
