
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Enums;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {

        Task<User> CheckIfCPF_CNPJExist(string cpf_cnpj);
        Task<User> CheckIfdEmailExist(string email);
        Task<User> CheckIfNicknameExist(string surname);
        Task<User> CheckDataLogin(string userName, string password);

        Task<IEnumerable<User>> GetAllUserByType(int quantity, UserRole role);
    }
}
