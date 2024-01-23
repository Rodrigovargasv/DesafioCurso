
using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using FluentValidation;

namespace DesafioCurso.Application.Validations.Person
{
    public class DeletePersonRequestValidation : AbstractValidator<DeletePesonRequest>
    {
        private readonly IPersonRepository _dbContext;

        public DeletePersonRequestValidation(IPersonRepository dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.IdOrIdentifier)
               .MustAsync(async (request, cancellationToken) =>
               {
                   var personIdOrIndentifer = await _dbContext.GetById(request);

                   if (personIdOrIndentifer == null)
                       throw new NotFoundException("Pessoa não encontrada");

                   return true;
               });
        }
    }
}
