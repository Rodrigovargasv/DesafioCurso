using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Enums;
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

        // Verifica se CPF ou CNPJ já existe no banco de dados
        public async Task<User> CheckIfCPF_CNPJExist(string cpf_cnpj)
        {
            if (cpf_cnpj == null)
                return null;

            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Cpf_Cnpj == cpf_cnpj);
        }

        // Verifica se Email já existe no banco de dados
        public async Task<User> CheckIfdEmailExist(string email)
        {
            if (email == null)
                return null;

            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        // Verifica se apelido já existe no banco de dados
        public async Task<User> CheckIfNicknameExist(string nickname)
        {
            if (nickname == null)
                return null;

            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Nickname == nickname);
        }

        // Verifica se apelido o email existe no banco de dados para realizar o login no sistema
        public async Task<User> CheckDataLogin(string userName)
        {
            if (userName == null)
                return null;

            // Recebe o resultado da verificação se o email é valido
            bool isEmail = IsValidEmail(userName);

            if (isEmail)
                return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == userName);

            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Nickname == userName);
        }

        // Verifica se é email o que o usuário digitou é valido
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

        // Busca usuário pelo seu tipo recebido via parâmetro.
        public async Task<IEnumerable<User>> GetAllUserByType(int quantity, UserRole role)
        {
            var permission = await _context.Permissions
                .Where(p => p.Role == role).ToListAsync();

            var userIds = permission.Select(p => p.UserId).ToList();

            var user = await _context.Users.Where(u => userIds.Contains(u.Id)).Take(quantity).ToListAsync();

            return user;
        }
    }
}