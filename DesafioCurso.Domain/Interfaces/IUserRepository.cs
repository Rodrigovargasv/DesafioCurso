
using DesafioCurso.Domain.Entities;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {

        Task<User> CheckIfCPF_CNPJExist(string cpf_cnpj);
        Task<User> CheckIfdEmailExist(string email);
        Task<User> CheckIfNicknameExist(string surname);
        Task<User> CheckDataLogin(string userName, string password);
    }
}
