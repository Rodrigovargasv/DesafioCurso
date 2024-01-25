using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Commons;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Application.Validations.Product
{
    public class CreateProductRequestValidation : AbstractValidator<CreateProductRequest>
    {
        private readonly ApplicationDbContext _context;

        public CreateProductRequestValidation(ApplicationDbContext context)
        {
            _context = context;

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
                .Length(13)
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