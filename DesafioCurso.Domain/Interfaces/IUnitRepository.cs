using DesafioCurso.Domain.Entities;


namespace DesafioCurso.Domain.Interfaces
{
    public interface IUnitRepository : IRepositoryBase<Unit>
    {
        Task<Unit> PropertyAcronymExists(string acronym);
    }
}
