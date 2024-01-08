using DesafioCurso.Infra.Ioc.ContextDB;
using DesafioCurso.Infra.Ioc.FluentValidation;
using DesafioCurso.Infra.Ioc.GlobalExecptions;
using DesafioCurso.Infra.Ioc.JWT;
using DesafioCurso.Infra.Ioc.Mediator;
using DesafioCurso.Infra.Ioc.Repository;
using DesafioCurso.Infra.Ioc.Service;
using DesafioCurso.Infra.Ioc.Swagger;
using DesafioCurso.Infra.Ioc.UnitOfWorkDependecy;
using Microsoft.AspNetCore.Builder;
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

            services.AddServiceGlobalExecptions();

            services.AddServiceValidationDomains();

            services.AddServiceJwtAuthenticationAndAutorization(configuration);

            services.AddServiceSwagger();

            services.AddServiceGeneratorShortId();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
        {
            builder.UseGlobalExceptionMiddleware();
            return builder;
        }
    }
}