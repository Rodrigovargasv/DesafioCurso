using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Application.Handlers;
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
            
            // Adiciona o MediatR e registra os handlers no assembly atual
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Registra o handler para CreateUnitRequest
            services.AddScoped<IRequestHandler<CreateUnitRequest, CreateUnitResponse>, CreateUnitHandler>();


            // Corrige o registro do handler para CreatePersonRequest
            services.AddScoped<IRequestHandler<CreatePersonRequest, CreatePersonResponse>, CreatePesonHandler>();


            services.AddTransient<IRequestHandler<GetAllUnitRequest, IEnumerable<GetAllUnitResponse>>, GetAllUnitHandler>();


            return services; ;

        }
    }
}
