using DesafioCurso.Domain.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.FluentValidation
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceValidationDomains(this IServiceCollection services)
        {
            // Configurado serviceço de validação com fluent validation
            services.AddScoped<UnitValidation>();
            services.AddScoped<PersonValidation>();
            services.AddScoped<ProductValidation>();
            services.AddScoped<UserValidation>();

            return services;
        }
    }
}
