﻿using DesafioCurso.Infra.Ioc.ContextDB;
using DesafioCurso.Infra.Ioc.Mediator;
using DesafioCurso.Infra.Ioc.Repository;
using DesafioCurso.Infra.Ioc.UnitOfWorkDependecy;
using DesafioCurso.Infra.Ioc.GlobalExecptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;


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

            return services;
        }

    
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
        {
            builder.UseGlobalExceptionMiddleware();
            return builder;
        }
    }
}
