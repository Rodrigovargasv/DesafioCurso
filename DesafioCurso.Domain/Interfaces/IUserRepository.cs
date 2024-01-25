using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Enums;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
     
        Task<User> CheckDataLogin(string userName);

        Task<IEnumerable<User>> GetAllUserByType(int page, int pageSize, UserRole role);
    }
}