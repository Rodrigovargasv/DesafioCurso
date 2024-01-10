using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.UnitOfWorkDependecy
{
    internal static class Startup
    {
        // Configura serviço do padrão unit of work.
        internal static IServiceCollection AddServiceUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }
    }
}