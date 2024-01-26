using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork<SqliteDbcontext> _uow;

        public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork<SqliteDbcontext> uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            // Verifica se a usuário existe
            var userId = await _userRepository.GetById(request.IdOrIdentifier);

            _userRepository.Delete(userId);
            await _uow.Commit();

            return userId.Adapt<DeleteUserResponse>();
        }
    }
}