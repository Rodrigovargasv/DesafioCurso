
using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.ProductHandler
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, DeleteProductResponse>
    {
        private readonly IProductRepository _context;
        private readonly IUnitOfWork _uow;

        public DeleteProductHandler(IProductRepository context, IUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var productId = await _context.GetById(request.Id);

            if (productId == null) throw new NotFoundException("Produto não encontrado");

            _context.Delete(productId);
            await _uow.Commit();

            return productId.Adapt<DeleteProductResponse>();


        }
    }
}
