using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using FluentValidation;

namespace DesafioCurso.Application.Validations.User
{
    public class DeleteUserRequestValidation : AbstractValidator<DeleteUserRequest>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserRequestValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;

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