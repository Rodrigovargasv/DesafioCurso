using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{

    public class UserRepository : RepositoryBase<User, SqliteDbcontext>, IUserRepository
    {
        private readonly SqliteDbcontext _context;

        public UserRepository(SqliteDbcontext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> ChecksIfCPF_CNPJAndEmailAndSurnameExists(string cpf_cnpj, string email, string surname)
        {
            if (cpf_cnpj == null || email == null || surname == null)
                return null;

            return await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(u => u.Cpf_Cnpj == cpf_cnpj || u.Email == email || u.Surname == surname);
        }
       
    }
}
