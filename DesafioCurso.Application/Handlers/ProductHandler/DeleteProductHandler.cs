using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.ProductHandler
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, DeleteProductResponse>
    {
        private readonly IProductRepository _context;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;

        public DeleteProductHandler(IProductRepository context, IUnitOfWork<ApplicationDbContext> uow)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            // Verifica se produto existe no banco de dados
            var productId = await _context.GetById(request.IdOrIdentifier);

            if (productId == null) throw new NotFoundException("Produto não encontrado");

            _context.Delete(productId);

            await _uow.Commit();

            return productId.Adapt<DeleteProductResponse>();
        }
    }
}