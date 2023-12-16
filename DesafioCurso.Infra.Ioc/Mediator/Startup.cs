using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Application.Handlers;
using DesafioCurso.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace DesafioCurso.Infra.Ioc.Mediator
{
    internal static class Startup
    {
        // Configura o serviço de injeção de dependência do mediator
        internal static IServiceCollection AddServiceMediator(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddScoped<IRequestHandler<CreateUnitRequest, CreateUnitResponse>, CreateUnitHandler>();

            return services;
        }
    }
}
