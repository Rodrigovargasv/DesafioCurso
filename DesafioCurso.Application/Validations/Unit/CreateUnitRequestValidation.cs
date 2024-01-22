using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DesafioCurso.Application.Validations.Unit
{
    public class CreateUnitRequestValidation : AbstractValidator<CreateUnitRequest>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateUnitRequestValidation(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;


            RuleFor(u => u.Acronym)
           .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo senha não pode conter espaço em branco.")
           .MustAsync(async (request, cancellationToken)
                => await _dbContext.Units.AsNoTracking().FirstOrDefaultAsync(x => x.Acronym == request.ToLower()) != null)
           .WithMessage("Esta unidade já existe")
           .NotEmpty()
           .NotNull();

            RuleFor(u => u.Decription)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo senha não pode conter espaço em branco.")
                .NotEmpty()
                .NotNull();
        }
    }
}
