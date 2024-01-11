using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Application.Interfaces;
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
        private readonly IPasswordManger _passwordManger;

        public LoginUserHandler(IUserRepository userRepository, TokenService tokenService,
            IUserPermissionRepository userPermissionRepository, IPasswordManger passwordManger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userPermissionRepository = userPermissionRepository;
            _passwordManger = passwordManger;
        }

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            #region Verifica se usuário existe no banco de dados tem permissão para acessar o sistema.

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                throw new BadRequestException("Dados inválidos");

            var userLogin = await _userRepository.CheckDataLogin(request.UserName.ToLower());

            var verifyPassword = _passwordManger.VerifyPassword(userLogin.Password, request.Password);

            if (userLogin == null || verifyPassword == false)
                throw new BadRequestException("Usuário ou senha inválido.");

            var userPermission = await _userPermissionRepository.VerifyIfUserExist(userLogin.Id);

            if (userPermission == null)
                throw new NotFoundException("Usuário não encontrado.");

            #endregion Verifica se usuário existe no banco de dados tem permissão para acessar o sistema.

            // Gera o token de autenticação usando o serviço de token
            var token = await _tokenService.GenerateToken(userLogin, userPermission);

            return new LoginUserResponse() { Token = token };
        }
    }
}