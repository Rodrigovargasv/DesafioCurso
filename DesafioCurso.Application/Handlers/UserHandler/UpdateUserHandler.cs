using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork<SqliteDbcontext> _uow;
        private readonly IPasswordManger _passwordManger;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork<SqliteDbcontext> uow,
            IPasswordManger passwordManger)
        {
            _userRepository = userRepository;
            _uow = uow;
            _passwordManger = passwordManger;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetById(request.IdOrIdentifier);

            // Verifica se o usuário existe
            if (userId is null)
                throw new NotFoundException("Usuário não encontrado");


            if (!string.IsNullOrEmpty(request.FullName))
                userId.FullName = request.FullName;

            if (!string.IsNullOrEmpty(request.NickName))
                userId.Nickname = request.NickName;

            if (!string.IsNullOrEmpty(request.Email))
                userId.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Password))
                userId.Password = request.Password;

            if (!string.IsNullOrEmpty(request.Cpf_Cnpj))
                userId.Cpf_Cnpj = request.Cpf_Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            userId.Password = _passwordManger.HashPassword(request.Password);

            _userRepository.Update(userId);
            await _uow.Commit();

            return userId.Adapt<UpdateUserResponse>();
        }
    }
}