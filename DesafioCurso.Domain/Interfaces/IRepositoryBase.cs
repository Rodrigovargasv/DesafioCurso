
namespace DesafioCurso.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(int quantidade);
        Task<TEntity> GetById(Guid? id);

        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
      
    }
}
