using DesafioCurso.Application.Validations.Unit;
using DesafioCurso.Domain.Validations;
using FluentValidation;
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

            // Registra os validadores FluentValidation
            services.AddValidatorsFromAssemblyContaining<CreateUnitRequestValidation>();

            services.AddValidatorsFromAssemblyContaining<UpdateUnitRequestValidation>();

            services.AddValidatorsFromAssemblyContaining<DeleteUnitRequestValidation>();

            return services;
        }
    }
}