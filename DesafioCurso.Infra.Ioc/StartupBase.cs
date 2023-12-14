
using DesafioCurso.Infra.Ioc.ContextDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc
{
    public static class StartupBase
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Serviço de banco de dados
            services.AddServicesDbContext(configuration);

            return services;
        }
    }
}
