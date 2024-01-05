using DesafioCurso.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DesafioCurso.Infra.Ioc.JWT
{
    public static class Startup
    {
        internal static IServiceCollection AddServiceJwtAuthenticationAndAutorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<TokenService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;

                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                       //ValidateIssuerSigningKey = true,
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true
                   };
               });

            // Configura os perfis de acesso aceitos pela aplicação
            services.AddAuthorization(option =>
            {
                option.AddPolicy("administrator", p => p.RequireRole("administrator"));
                option.AddPolicy("commonUser", p => p.RequireRole("commonUser"));
                option.AddPolicy("manager", p => p.RequireRole("manager"));
                option.AddPolicy("seller", p => p.RequireRole("seller"));
            });

            return services;
        }
    }
}