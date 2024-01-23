
using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Domain.Common.Exceptions;
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
                .MustAsync(async (request, cancellationToken) =>
                {
                    var idOrIdentifier  = await _unitRepository.GetById(request);

                    if (idOrIdentifier == null)
                        throw new NotFoundException("Unidade não encontrada");

                    return true;
                });
               
        }
    }
}
