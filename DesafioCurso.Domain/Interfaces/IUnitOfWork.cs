
namespace DesafioCurso.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        void Rollback();
    }
}
