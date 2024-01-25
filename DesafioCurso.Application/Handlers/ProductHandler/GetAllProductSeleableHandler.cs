using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

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
            // Busca por todo os produtos vendaveis no banco de dados, sendo limitado pela quantidade que o usuário informar no request.
            var products = await _productRepository.GetAllProductsSaleables(request.Paramenters.Page, request.Paramenters.PageSize);

            var productResponse = products.Adapt<IEnumerable<GetAllProductSeleableResponse>>();

            return productResponse;
        }
    }
}