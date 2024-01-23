
using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Application.Validations.Product
{
    public class UpdateProductRequestValidation : AbstractValidator<UpdateProductRequest>
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;

        public UpdateProductRequestValidation(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;

            RuleFor(x => x.IdOrIdentifier)
                .MustAsync(async (request, CancellationToken) =>
                {
                    var productIdOrIdentifier = await _productRepository.GetById(request);

                    if (productIdOrIdentifier == null)
                        throw new NotFoundException("Produto não encontrado");

                    return true;
                });

            RuleFor(x => x.FullDescription)
               .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo descrição completa não pode conter espaço em branco.")
               .NotNull()
               .NotEmpty()
               .MaximumLength(150);

            RuleFor(x => x.BriefDescription)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo descrição resumida não pode conter espaço em branco.")
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.AcronynmUnit)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo sigla da unidade não pode conter espaço em branco.")
                .NotEmpty()
                .NotNull()
                .MaximumLength(10)
                .MustAsync(async (request, cancellationToken) =>
                    await _context.Units.AsNoTracking().AnyAsync(x => x.Acronym == request.ToUpper())
                        ? true : throw new BadRequestException("A unidade informada não existe, cadastre uma unidade ou tente novamente."));

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.BarCode)
                .Must(barCode =>
                {
                    if(string.IsNullOrEmpty(barCode))
                        return true;

                    if (barCode.Length < 13)
                        throw new CustomException("O código de barras deve ter no mímino 13 caracteres.");

                    return true;

                })
                .MustAsync(async (request, cancellationToken) =>
                {
                    if (string.IsNullOrEmpty(request))
                        return true;

                    return await _context.Products.AsNoTracking().AnyAsync(x => x.BarCode == request)
                         ? throw new BadRequestException("Já existe um código de barras com estas informações.") : true;
                });
        }
    }
}
