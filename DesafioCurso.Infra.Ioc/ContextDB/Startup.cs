﻿using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;

namespace DesafioCurso.Infra.Ioc.ContextDB
{
    internal static class Startup
    {
        internal static IServiceCollection AddServicesDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurações banco de dados PostgreSql
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Altera o formato de data e hora do banco de dados PostgreSql
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Configurações do banco de dados SQLite
            services.AddDbContext<SqliteDbcontext>(options =>
               options.UseSqlite(configuration.GetConnectionString("SqliteConnection"),
                   builder => builder.MigrationsAssembly(typeof(SqliteDbcontext).Assembly.FullName)));



            return services;

        }
    }
}
