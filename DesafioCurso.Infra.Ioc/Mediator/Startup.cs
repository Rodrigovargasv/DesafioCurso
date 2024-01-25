using DesafioCurso.Application.Behaviors;
using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Request.UserPermission;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Application.Commands.Response.UserPermission;
using DesafioCurso.Application.Handlers.PersonHandler;
using DesafioCurso.Application.Handlers.ProductHandler;
using DesafioCurso.Application.Handlers.UnitHandler;
using DesafioCurso.Application.Handlers.UserHandler;
using DesafioCurso.Application.Handlers.UserPermissionHandler;
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
            #region Handlers de unidade

            // Registra o handler para CreateUnitRequest
            services.AddScoped<IRequestHandler<CreateUnitRequest, CreateUnitResponse>, CreateUnitHandler>();

            // Registra o handler para GetAllUnitHandler
            services.AddScoped<IRequestHandler<GetAllUnitRequest, IEnumerable<GetAllUnitResponse>>, GetAllUnitHandler>();

            // Registra o handler para GetUnitByIdHandler
            services.AddScoped<IRequestHandler<GetUnitByIdRequest, GetUnitByIdResponse>, GetUnitByIdHandler>();

            // Registra o handler para UpdateUnitHandler
            services.AddScoped<IRequestHandler<UpdateUnitRequest, UpdateUnitResponse>, UpdateUnitHandler>();

            // Registra o handler para DelegatingHandler
            services.AddScoped<IRequestHandler<DeleteUnitRequest, DeleteUnitResponse>, DeleteUnitHandler>();

            #endregion Handlers de unidade

            #region Handlers de Person

            // Registra o handler para CreatePersonRequest
            services.AddScoped<IRequestHandler<GetAllPersonRequest, IEnumerable<GetAllPersonResponse>>, GetAllPersonHandler>();

            // Registra o handler para GetPersonByIdHandler
            services.AddScoped<IRequestHandler<GetPersonByIdRequest, GetPersonByIdResponse>, GetPersonByIdHandler>();

            services.AddScoped<IRequestHandler<CreatePersonRequest, CreatePersonResponse>, CreatePersonHandler>();

            // Registra o handler para UpdatePersonHandler
            services.AddScoped<IRequestHandler<UpdatePersonRequest, UpdatePersonResponse>, UpdatePersonHandler>();

            // Registra o handler para DeletePersonHandler
            services.AddScoped<IRequestHandler<DeletePesonRequest, DeletePersonResponse>, DeletePersonHandler>();

            #endregion Handlers de Person

            #region Handlers de Product

            services.AddScoped<IRequestHandler<CreateProductRequest, CreateProductResponse>, CreateProductHandler>();

            services.AddScoped<IRequestHandler<UpdateProductRequest, UpdateProductResponse>, UpdateProductHandler>();

            services.AddScoped<IRequestHandler<GetAllProductRequest, IEnumerable<GetAllProductResponse>>, GetAllProductHandler>();
            services.AddScoped<IRequestHandler<GetAllProductSeleableRequest, IEnumerable<GetAllProductSeleableResponse>>, GetAllProductSeleableHandler>();

            services.AddScoped<IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>, GetProductByIdHandler>();

            services.AddScoped<IRequestHandler<DeleteProductRequest, DeleteProductResponse>, DeleteProductHandler>();

            #endregion Handlers de Product

            #region Handlers de User

            services.AddScoped<IRequestHandler<CreateUserRequest, CreateUserResponse>, CreateUserHandler>();
            services.AddScoped<IRequestHandler<GetAllUserRequest, IEnumerable<GetAllUserResponse>>, GetAllUserHandler>();
            services.AddScoped<IRequestHandler<GetAllUserTypeSellerRequest, IEnumerable<GetAllUserTypeSellerResponse>>, GetAllUserTypeSellerHandler>();
            services.AddScoped<IRequestHandler<GetAllUserTypeManagerRequest, IEnumerable<GetAllUserTypeManagerResponse>>, GetAllUserTypeManagerHandler>();
            services.AddScoped<IRequestHandler<GetAllUserTypeAdministratorRequest, IEnumerable<GetAllUserTypeAdministradorResponse>>, GetAllUserTypeAdministratorHandler>();
            services.AddScoped<IRequestHandler<UpdateUserRequest, UpdateUserResponse>, UpdateUserHandler>();
            services.AddScoped<IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>, GetUserByIdHandler>();
            services.AddScoped<IRequestHandler<DeleteUserRequest, DeleteUserResponse>, DeleteUserHandler>();

            #endregion Handlers de User

            #region Handlers de UserPermission

            services.AddScoped<IRequestHandler<UpdateUserPermissionRequest, UpdateUserPermissionResponse>, UpdateUserPermissionHandler>();
            services.AddScoped<IRequestHandler<GetAllUserPermissionRequest, IEnumerable<GetAllUserPermissionResponse>>, GetAllUserPermissionHandler>();

            #endregion Handlers de UserPermission

            // Handlers de Login User
            services.AddScoped<IRequestHandler<LoginUserRequest, LoginUserResponse>, LoginUserHandler>();

            // Configura serviço do MediatR e registra os handlers no assembly atual
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}