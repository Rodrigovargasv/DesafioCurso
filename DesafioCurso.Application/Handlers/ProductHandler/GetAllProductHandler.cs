using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.ProductHandler
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductRequest, IEnumerable<GetAllProductResponse>>
    {
        private readonly IProductRepository _context;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            _context = productRepository;
        }

        public async Task<IEnumerable<GetAllProductResponse>> Handle(GetAllProductRequest request, CancellationToken cancellationToken)
        {
            // Busca por todos os produtos no banco de dados, sendo limitado pela quantidade que o usuário informar no request.
            var products = await _context.GetAll(request.Quantity);

            var productResponse = products.Adapt<IEnumerable<GetAllProductResponse>>();

            return productResponse;
        }
    }
}