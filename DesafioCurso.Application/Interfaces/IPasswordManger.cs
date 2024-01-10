
namespace DesafioCurso.Application.Interfaces
{
    public interface IPasswordManger
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
