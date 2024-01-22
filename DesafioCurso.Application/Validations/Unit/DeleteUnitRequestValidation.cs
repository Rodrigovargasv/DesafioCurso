
using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Domain.Interfaces;
using FluentValidation;

namespace DesafioCurso.Application.Validations.Unit
{
    public class DeleteUnitRequestValidation : AbstractValidator<DeleteUnitRequest>
    {

        private readonly IUnitRepository _unitRepository;

        public DeleteUnitRequestValidation(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;

            RuleFor(x => x.IdOrIdentifier)
                .MustAsync(async (request, cancellationToken) => await _unitRepository.GetById(request) != null)
                .WithMessage("Unidade não encontrada");
        }
    }
}
