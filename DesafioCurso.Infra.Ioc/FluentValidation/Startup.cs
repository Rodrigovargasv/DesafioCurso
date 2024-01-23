using DesafioCurso.Application.Validations.Person;
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
            services.AddScoped<ProductValidation>();
            services.AddScoped<UserValidation>();
            services.AddScoped<UserPermissionValidation>();


            #region Registra os validadores FluentValidation de unidade
            services.AddValidatorsFromAssemblyContaining<CreateUnitRequestValidation>();

            services.AddValidatorsFromAssemblyContaining<UpdateUnitRequestValidation>();

            services.AddValidatorsFromAssemblyContaining<DeleteUnitRequestValidation>();

            #endregion

            #region #region Registra os validadores FluentValidation de pessoa
            services.AddValidatorsFromAssemblyContaining<CreatePersonRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<UpdatePersonRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetPersonByIdRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<DeletePersonRequestValidation>();
            #endregion


            return services;
        }
    }
}