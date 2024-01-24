using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Domain.Common.Exceptions;
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
                .MustAsync(async (request, cancellationToken) =>
                {
                    var idOrIdentifier = await _unitRepository.GetById(request);

                    if (idOrIdentifier == null)
                        throw new NotFoundException("Unidae não encontrada.");

                    return true;
                });

            RuleFor(x => x.Decription)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo senha não pode conter espaço em branco.");



        }


    }
}
