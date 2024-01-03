
using DesafioCurso.Domain.Entities;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {

        Task<User> ChecksIfCPF_CNPJAndEmailAndSurnameExists(string cpf_cnpj, string email, string surname);
    }
}
