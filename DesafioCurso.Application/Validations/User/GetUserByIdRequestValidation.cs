

using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;

namespace DesafioCurso.Application.Validations.User
{
    public class GetUserByIdRequestValidation : AbstractValidator<GetUserByIdRequest>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdRequestValidation(IUserRepository context)
        {
            _userRepository = context;

            RuleFor(x => x.IdOrIdentifier)
                .MustAsync(async (request, cancellationToken) =>
                {
                    var idOrIdentifier = await _userRepository.GetById(request);

                    if (idOrIdentifier == null)
                        throw new NotFoundException("Usuário não encontrado.");

                    return true;
                });
        }
    }
}
