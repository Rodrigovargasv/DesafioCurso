using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.ProductHandler
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductValidation _productValidations;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly IUnitRepository _unitRepository;

        public UpdateProductHandler(IProductRepository productRepository, ProductValidation productValidations, IUnitOfWork<ApplicationDbContext> uow, IUnitRepository unitRepository)
        {
            _productRepository = productRepository;
            _productValidations = productValidations;
            _uow = uow;
            _unitRepository = unitRepository;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var productId = await _productRepository.GetById(request.IdOrIdentifier);

            if (!string.IsNullOrEmpty(request.FullDescription))
                productId.FullDescription = request.FullDescription;

            if (!string.IsNullOrEmpty(request.BriefDescription))
                productId.BriefDescription = request.BriefDescription;

            if (!string.IsNullOrEmpty(request.Price.ToString()))
                productId.Price = request.Price;

            if (!string.IsNullOrEmpty(request.QuantityStock.ToString()))
                productId.QuantityStock = request.QuantityStock;

            if (!string.IsNullOrEmpty(request.BarCode))
                productId.BarCode = request.BarCode;

            if (!string.IsNullOrEmpty(request.Saleable.ToString()))
                productId.Saleable = request.Saleable;

            if (!string.IsNullOrEmpty(request.Active.ToString()))
                productId.Active = request.Active;

            if (!string.IsNullOrEmpty(request.AcronynmUnit))
                productId.AcronynmUnit = request.AcronynmUnit;

            // Passa a sigla da unidade para maiúculo.
            productId.AcronynmUnit = request.AcronynmUnit.ToUpper();

            _productRepository.Update(productId);
            await _uow.Commit();

            return productId.Adapt<UpdateProductResponse>();
        }
    }
}