using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Application.Services;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public LoginUserHandler(IUserRepository userRepository, TokenService tokenService,
            IUserPermissionRepository userPermissionRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userPermissionRepository = userPermissionRepository;

        }
        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            #region Verifica se usuário existe no banco de dados tem permissão para acessar o sistema.

            var userLogin = await _userRepository.CheckDataLogin(request.UserName.ToLower(), request.Password);

            if (userLogin == null)
                throw new BadRequestException("Usuário ou senha inválido.");

            var userPermission = await _userPermissionRepository.VerifyIfUserExist(userLogin.Id.Value);

            if (userPermission == null)
                throw new NotFoundException("Usuário não encontrado.");
            #endregion


            // Gera o token de autenticação usando o serviço de token
            var token = await _tokenService.GenerateToken(userLogin, userPermission);

            return new LoginUserResponse() { Token = token };

        }
    }
}