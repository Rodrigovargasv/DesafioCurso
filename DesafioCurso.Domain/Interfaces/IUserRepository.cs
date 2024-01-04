
using DesafioCurso.Domain.Entities;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {

        Task<User> CheckIfCPF_CNPJAndEmailAndSurnameExists(string cpf_cnpj, string email, string surname);
        Task<User> CheckDataLogin(string userName, string password);
    }
}
