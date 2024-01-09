using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DesafioCurso.Infra.Ioc.Swagger
{
    internal static class Startup
    {
        internal static IServiceCollection AddServiceSwagger(this IServiceCollection services)
        {
            // Adicionado o serviço de autenticação com JWT bearer.
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioCurso", Version = "v1" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira 'bearer' e um token JWT válido."
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }
    }
}