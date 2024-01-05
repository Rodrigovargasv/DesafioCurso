
using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Application.Handlers.ProductHandler
{
    public class GetAllProductSeleableHandler : IRequestHandler<GetAllProductSeleableRequest, IEnumerable<GetAllProductSeleableResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductSeleableHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<GetAllProductSeleableResponse>> Handle(GetAllProductSeleableRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsSaleables(request.Quantity);

            var productResponse = products.Adapt<IEnumerable<GetAllProductSeleableResponse>>();

            return productResponse;
        }
    }
}
