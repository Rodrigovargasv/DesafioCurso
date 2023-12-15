
namespace DesafioCurso.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetbyId(int id);

        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
