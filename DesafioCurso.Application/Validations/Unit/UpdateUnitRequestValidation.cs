using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Interfaces;
using FluentValidation;

namespace DesafioCurso.Application.Validations.Unit
{
    public class UpdateUnitRequestValidation : AbstractValidator<UpdateUnitRequest>
    {
        private readonly IUnitRepository _unitRepository;

        public UpdateUnitRequestValidation(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;

            RuleFor(x => x.IdOrIdentifier)
                .MustAsync(async (request, cancellationToken) => await _unitRepository.GetById(request) != null)
                .WithMessage("Unidade não encontrada");

            RuleFor(x => x.Decription)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo senha não pode conter espaço em branco.");



        }


    }
}
