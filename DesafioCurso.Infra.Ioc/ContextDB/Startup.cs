
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.ContextDB
{
    internal static class Startup
    {
        internal static IServiceCollection AddServicesDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Altera o formato de data e hora do banco de dados PostgreSql
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;

        }
    }
}
