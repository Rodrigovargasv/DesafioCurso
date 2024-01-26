using DesafioCurso.Application.Validations.Login;
using DesafioCurso.Application.Validations.Person;
using DesafioCurso.Application.Validations.Product;
using DesafioCurso.Application.Validations.Unit;
using DesafioCurso.Application.Validations.User;
using DesafioCurso.Application.Validations.UserPermission;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioCurso.Infra.Ioc.FluentValidation
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceValidationDomains(this IServiceCollection services)
        {
            #region Registra os validadores FluentValidation de unidade

            services.AddValidatorsFromAssemblyContaining<CreateUnitRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<UpdateUnitRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<DeleteUnitRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllUnitRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetUnitByIdRequestValidation>();

            #endregion Registra os validadores FluentValidation de unidade

            #region #region Registra os validadores FluentValidation de pessoa

            services.AddValidatorsFromAssemblyContaining<CreatePersonRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<UpdatePersonRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetPersonByIdRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllPersonRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<DeletePersonRequestValidation>();

            #endregion #region Registra os validadores FluentValidation de pessoa

            #region #region Registra os validadores FluentValidation de produto

            services.AddValidatorsFromAssemblyContaining<CreatePersonRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<UpdateProductRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetProductByIdRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllProductRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllProductRequestSeleableValidation>();
            services.AddValidatorsFromAssemblyContaining<DeleteProductRequestValidation>();

            #endregion #region Registra os validadores FluentValidation de produto

            #region #region Registra os validadores FluentValidation de usuário

            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetUserByIdRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<DeleteUserRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllUserTypeSellerRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllUserTypeManagerRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllUserTypeAdministratorRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllUserRequestValidation>();

            #endregion #region Registra os validadores FluentValidation de usuário

            #region #region Registra os validadores FluentValidation de permissão de usuário

            services.AddValidatorsFromAssemblyContaining<UpdateUserPermissionRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<GetAllUserPermissionRequestValidation>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidation>();

            #endregion #region Registra os validadores FluentValidation de permissão de usuário

            return services;
        }
    }
}