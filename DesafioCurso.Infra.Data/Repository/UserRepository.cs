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

        public async Task<User> CheckIfCPF_CNPJAndEmailAndSurnameExists(string cpf_cnpj, string email, string surname)
        {
            if (cpf_cnpj == null || email == null || surname == null)
                return null;

            return await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(u => u.Cpf_Cnpj == cpf_cnpj || u.Email == email || u.Nickname == surname);
        }

        public async Task<User> CheckDataLogin(string userName, string password)
        {
            if (userName == null || password == null)
                return null;

            // Recebe o resultado da verificação se o email é valido
            bool isEmail = IsValidEmail(userName);

            if(isEmail)
                return await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(u => u.Email == userName && u.Password == password);

    
            return await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(u => u.Nickname == userName && u.Password == password);
        }

        // verifica se é email o que o usuário digitou
        private bool IsValidEmail(string userName)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(userName);
                return addr.Address == userName;
            }
            catch
            {
                return false;
            }
        }

    }
}
