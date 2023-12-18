using DesafioCurso.Domain.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.FluentValidation
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceValidationDomains(this IServiceCollection services)
        {
            services.AddScoped<UnitValidation>();
            return services;
        }
    }
}
