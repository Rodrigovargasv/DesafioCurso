
using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.ProductHandler
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
    {
        private readonly IProductRepository _context;

        public GetProductByIdHandler(IProductRepository context)
        {
            _context = context;
        }

        public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var productId = await _context.GetById(request.Id);

            if (productId is null)
                throw new NotFoundException("Produto não encontrado.");

            return productId.Adapt<GetProductByIdResponse>();
        }
    }
}
