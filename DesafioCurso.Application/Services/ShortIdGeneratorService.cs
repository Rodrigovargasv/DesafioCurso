using DesafioCurso.Application.Interfaces;

namespace DesafioCurso.Application.Services
{
    public class ShortIdGeneratorService : IShortIdGeneratorService
    {
        public string GenerateShortId()
        {
            // Gera um GUID (Globally Unique Identifier)
            Guid guid = Guid.NewGuid();

            // Converte o GUID para uma sequência de bytes
            byte[] guidBytes = guid.ToByteArray();

            // Converte os bytes para base64
            string base64String = Convert.ToBase64String(guidBytes);

            // Remove caracteres especiais e espaços em branco
            base64String = base64String.Replace("/", "_").Replace("+", "").Replace("=", "").Replace("-", "");

            // Pega os primeiros 10 caracteres
            string shortId = base64String.Substring(0, Math.Min(base64String.Length, 10));

            return shortId;
        }
    }
}