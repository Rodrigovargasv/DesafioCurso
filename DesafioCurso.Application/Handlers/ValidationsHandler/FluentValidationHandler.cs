using FluentValidation;
using MediatR;

namespace DesafioCurso.Domain.Handlers.ValidationsHandler
{
    public class FluentValidationHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FluentValidationHandler(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Execute as validações antes de prosseguir
            foreach (var validator in _validators)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    // Trate as falhas de validação, por exemplo, lançando uma exceção ou retornando uma resposta específica
                    throw new ValidationException(validationResult.Errors);
                }
            }

            // Se as validações passarem, prossiga com o pipeline
            return await next();
        }
    }
}