using DesafioCurso.Application.Interfaces;
using System.Text;

namespace DesafioCurso.Application.Services
{
    public class ShortIdGeneratorService : IShortIdGeneratorService
    {
        public string GenerateShortId()
        {
            // Gera um GUID (Globally Unique Identifier)
            var id = Guid.NewGuid().ToString("N").ToUpper();

            // Converte o GUID para base64
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(id));

            // Pega os primeiros 10 caracteres
            string shortId = base64String.Substring(0, Math.Min(base64String.Length, 10));

            return shortId;
        }
    }
}