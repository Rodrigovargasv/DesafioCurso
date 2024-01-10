using DesafioCurso.Domain.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.FluentValidation
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceValidationDomains(this IServiceCollection services)
        {
            // Configurado serviço de validação de entitdades utilizando fluent validation
            services.AddScoped<UnitValidation>();
            services.AddScoped<PersonValidation>();
            services.AddScoped<ProductValidation>();
            services.AddScoped<UserValidation>();
            services.AddScoped<UserPermissionValidation>();

            return services;
        }
    }
}