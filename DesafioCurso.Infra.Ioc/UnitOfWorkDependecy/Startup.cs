using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.UnitOfWorkDependecy
{
    internal static class Startup
    {
        
            internal static IServiceCollection AddServiceUnitOfWork(this IServiceCollection services)
            {
                services.AddTransient<IUnitOfWork, UnitOfWork>();

                return services;
            }
        
    }
}
