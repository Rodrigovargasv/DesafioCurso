using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.ProductHandler
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>

    {
        private readonly IProductRepository _productRepository;
        private readonly ProductValidation _productValidations;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly IShortIdGeneratorService _shortIdGeneratorService;

        private readonly IUnitRepository _unitRepository;

        public CreateProductHandler(IProductRepository productRepository, ProductValidation productValidations,
            IUnitOfWork<ApplicationDbContext> uow, IUnitRepository unitRepository, IShortIdGeneratorService shortIdGenerator)
        {
            _productRepository = productRepository;
            _productValidations = productValidations;
            _uow = uow;
            _unitRepository = unitRepository;
            _shortIdGeneratorService = shortIdGenerator;
        }

        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = request.Adapt<Product>();

            var unitValidation = await _productValidations.ValidateAsync(product);

            if (!unitValidation.IsValid) throw new ValidationException(unitValidation.Errors);

            var unit = await _unitRepository.PropertyAcronymExists(request.AcronynmUnit.ToUpper());

            if (unit is null)
                throw new NotFoundException("Unidade informada não existe no banco de dados." +
                    "Realize o cadastro da unidade em seu endpoint.");

            var barCode = await _productRepository.VerifyIfBarCodeExists(product.BarCode);

            if (barCode != null)
                throw new CustomException("Já existe um código de barras cadastrado com estas informações");

            product.AcronynmUnit = product.AcronynmUnit.ToUpper();

            product.Identifier = _shortIdGeneratorService.GenerateShortId();

            await _productRepository.Create(product);
            await _uow.Commit();

            return product.Adapt<CreateProductResponse>();
        }
    }
}