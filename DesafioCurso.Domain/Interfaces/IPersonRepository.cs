using DesafioCurso.Domain.Entities;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        Task<Person> PropertyDocumentExist(string document);

        Task<Person> PropertyAlternativeCodeExist(string AlternativeCode);
    }
}