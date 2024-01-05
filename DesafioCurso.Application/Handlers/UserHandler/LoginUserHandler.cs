using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Application.Services;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public LoginUserHandler(IUserRepository userRepository, TokenService tokenService, IUserPermissionRepository userPermissionRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var userLogin = await _userRepository.CheckDataLogin(request.UserName.ToLower(), request.Password);

            if (userLogin == null)
                throw new BadRequestException("Usuário ou senha inválido.");

            var userPermission = await _userPermissionRepository.VerifyIfUserExist(userLogin.Id.Value);

            // Gera o token de autenticação usando o serviço de token
            var token = await _tokenService.GenerateToken(userLogin, userPermission);

            return new LoginUserResponse() { Token = token };
        }
    }
}