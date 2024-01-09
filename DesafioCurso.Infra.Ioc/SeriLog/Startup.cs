
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DesafioCurso.Infra.Ioc.SeriLog
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceSeriLog(this IServiceCollection services)
        {
            // Configuração do logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/log.text", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(l =>
            {
                l.AddSerilog();
            });

            return services;

        }
    }
}
