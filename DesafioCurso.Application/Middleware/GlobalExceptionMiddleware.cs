using Microsoft.AspNetCore.Http;
using DesafioCurso.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Serilog;
using DesafioCurso.Domain.Common.Exceptions;

namespace DesafioCurso.Application.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next(context);

            }
            catch (Exception ex)
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
                Log.Error($"{errorResult.Exception} A solicitação falhou com o código de status {errorResult.StatusCode} e o  Error Id {errorId}.");
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = errorResult.StatusCode;
                await response.WriteAsync(JsonSerializer.Serialize(errorResult));
            }

        }
    }
}
