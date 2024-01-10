using DesafioCurso.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DesafioCurso.Application.Services
{
    public class PasswordManager : IPasswordManger
    {
        public string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
