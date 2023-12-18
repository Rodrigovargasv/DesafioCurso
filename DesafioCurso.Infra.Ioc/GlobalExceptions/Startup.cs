using DesafioCurso.Application.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.GlobalExecptions
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceGlobalExecptions(this IServiceCollection services) 
        {
            services.AddScoped<GlobalExceptionMiddleware>();
            return services;
        }

        internal static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }

    }
}
