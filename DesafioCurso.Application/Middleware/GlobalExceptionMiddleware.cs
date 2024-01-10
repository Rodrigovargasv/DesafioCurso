using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace DesafioCurso.Application.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

                if (context.Response.StatusCode is 401)
                {
                    _logger.LogWarning("Usuário não autenticado, é necessário realizar o login na api.");
                    throw new UnauthorizedException("Usuário não autenticado, realize o login no sistema e tente novamente.");
                }

                if (context.Response.StatusCode is 403)
                {
                    _logger.LogWarning("O usuário não tem permissão para acessar este recurso.");
                    throw new ForbiddenException("O usuário não tem permissão para acessar este recurso.");
                }
            }
            catch (Exception ex)
            {
                using (LogContext.PushProperty("ErrorId", Guid.NewGuid().ToString()))
                {
                    var errorId = Guid.NewGuid().ToString();

                    ErrorResult errorResult = new()
                    {
                        Source = ex.TargetSite?.DeclaringType?.FullName,
                        Exception = ex.Message.Trim(),
                        ErrorId = errorId,
                        SupportMessage = $"Forneça o ErrorId {errorId} à equipe de suporte para uma análise mais aprofundada."
                    };

                    if (ex is not CustomException && ex.InnerException != null)
                    {
                        while (ex.InnerException != null)
                        {
                            ex = ex.InnerException;
                        }
                    }

                    if (ex is FluentValidation.ValidationException fluentException)
                    {
                        errorResult.Exception = "Erro na requisição. Uma ou mais validações falharam.";
                        foreach (var error in fluentException.Errors)
                        {
                            errorResult.Messages.Add($"[{error.PropertyName}] {error.ErrorMessage}");
                        }
                    }

                    switch (ex)
                    {
                        case CustomException e:
                            errorResult.StatusCode = (int)e.StatusCode;
                            if (e.ErrorMessages is not null)
                            {
                                errorResult.Messages = e.ErrorMessages;
                            }

                            break;

                        case KeyNotFoundException:
                            errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                            break;

                        case ValidationException:
                            errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;

                        case FluentValidation.ValidationException:
                            errorResult.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                            break;

                        default:
                            errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }

                    Log.Error($"{errorResult.Exception} A solicitação falhou com o código de status {errorResult.StatusCode} e o Error Id {errorResult.ErrorId}.");

                    HttpResponse response = context.Response;
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsync(JsonSerializer.Serialize(errorResult));
                }
            }
        }
    }
}