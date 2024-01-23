using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.ProductHandler
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>

    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly IShortIdGeneratorService _shortIdGeneratorService;

        public CreateProductHandler(IProductRepository productRepository,
            IUnitOfWork<ApplicationDbContext> uow, IShortIdGeneratorService shortIdGenerator)
        {
            _productRepository = productRepository;
            _uow = uow;
            _shortIdGeneratorService = shortIdGenerator;
        }

        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = request.Adapt<Product>();

            // Alterar a singla da unidade sempre para maiúsculo;
            product.AcronynmUnit = product.AcronynmUnit.ToUpper();

            // Gerar identificador unico para cada produto criado.
            product.Identifier = _shortIdGeneratorService.GenerateShortId();

            await _productRepository.Create(product);

            await _uow.Commit();

            return product.Adapt<CreateProductResponse>();
        }
    }
}