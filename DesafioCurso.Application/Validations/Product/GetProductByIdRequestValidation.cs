using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using FluentValidation;

namespace DesafioCurso.Application.Validations.Product
{
    public class GetProductByIdRequestValidation : AbstractValidator<GetProductByIdRequest>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdRequestValidation(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.IdOrIdentifier)
                .MustAsync(async (request, cancellationToken) =>
                {
                    var productIdOrIndentifier = await _productRepository.GetById(request);

                    if (productIdOrIndentifier is null)
                        throw new NotFoundException("Produto não encontrado");

                    return true;
                });
        }
    }
}