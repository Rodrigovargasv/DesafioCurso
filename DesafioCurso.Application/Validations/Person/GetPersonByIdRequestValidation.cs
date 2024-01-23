
using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;

namespace DesafioCurso.Application.Validations.Person
{
    public class GetPersonByIdRequestValidation : AbstractValidator<GetPersonByIdRequest>
    {
        private readonly IPersonRepository _dbContext;

        public GetPersonByIdRequestValidation(IPersonRepository dbContext)
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
