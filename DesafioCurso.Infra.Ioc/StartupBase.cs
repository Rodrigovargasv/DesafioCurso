using DesafioCurso.Infra.Ioc.ContextDB;
using DesafioCurso.Infra.Ioc.Mediator;
using DesafioCurso.Infra.Ioc.Repository;
using DesafioCurso.Infra.Ioc.UnitOfWorkDependecy;
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
            services.AddServiceUnitOfWork();
            services.AddServiceRepository();
            services.AddServiceMediator();
            return services;
        }
    }
}
