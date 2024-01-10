
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.Service
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceGeneratorShortId(this IServiceCollection services)
        {
            services.AddScoped<IShortIdGeneratorService, ShortIdGeneratorService>();

            services.AddScoped<IPasswordManger, PasswordManager>();

            return services;
        }
    }
}
