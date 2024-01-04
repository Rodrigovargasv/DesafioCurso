using DesafioCurso.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioCurso.Application.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuracao;

        public TokenService(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }


        public async Task<string> GenerateToken(User user, UserPermission userPermission)
        {
            // Busca dados no appsettings
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.Development.json");

            //IConfigurationRoot configuration = builder.Build();


            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracao["Jwt:Key"])), SecurityAlgorithms.HmacSha256),

                Expires = DateTime.UtcNow.AddHours(double.Parse(_configuracao["Jwt:ExpireHours"])),

                Subject = GenerateClaims(user, userPermission)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }
        

        // Faz a gereção da Claims
        private static ClaimsIdentity GenerateClaims(User user, UserPermission userPermission)
        {
            var claims = new ClaimsIdentity();

            if(!string.IsNullOrEmpty(user.Email))
                claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            if (!string.IsNullOrEmpty(user.Nickname))
                claims.AddClaim(new Claim(ClaimTypes.Name, user.Nickname));

            claims.AddClaim(new Claim(ClaimTypes.Role, userPermission.Role.ToString()));

            return claims;
        }
    }
}
