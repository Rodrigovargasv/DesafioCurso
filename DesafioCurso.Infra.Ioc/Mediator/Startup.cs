using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Application.Handlers;
using DesafioCurso.Domain.Validations;
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


            // Registra o handler para CreatePersonRequest
            services.AddScoped<IRequestHandler<CreatePersonRequest, CreatePersonResponse>, CreatePesonHandler>();

            // Registra o handler para GetAllUnitHandler
            services.AddScoped<IRequestHandler<GetAllUnitRequest, IEnumerable<GetAllUnitResponse>>, GetAllUnitHandler>();

            // Registra o handler para UpdateUnitHandler
            services.AddScoped<IRequestHandler<UpdateUnitRequest, UpdateUnitResponse>, UpdateUnitHandler>();


            return services; ;

        }
    }
}
