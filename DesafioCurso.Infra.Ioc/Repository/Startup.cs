﻿using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.Repository
{
    internal static class Startup
    {
        // Configura serviço dos repositorios
        internal static IServiceCollection AddServiceRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();

            return services;
        }
    }
}