namespace DesafioCurso.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(int page, int pageSize);

        Task<TEntity> GetById(string value);

        Task Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}