using DesafioCurso.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DesafioCurso.Application.Services
{
    public class PasswordManager : IPasswordManger
    {
        // Faz a criptografia da senha utilizando hash
        public string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }

        // Faz a verificação de senha para ver se a senha recebida é igual a senha registrada no banco
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
