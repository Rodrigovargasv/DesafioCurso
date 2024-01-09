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
        private readonly ILogger<LoginUserHandler> _logger;

        public LoginUserHandler(IUserRepository userRepository, TokenService tokenService,
            IUserPermissionRepository userPermissionRepository, ILogger<LoginUserHandler> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userPermissionRepository = userPermissionRepository;
            _logger = logger;
        }
        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {

            try
            {
                _logger.LogInformation($"Iniciando manipulador LoginUserHandler para o usuário {request.UserName.ToLower()}");

                var userLogin = await _userRepository.CheckDataLogin(request.UserName.ToLower(), request.Password);


                if (userLogin == null)
                {
                    _logger.LogWarning($"Usuário {request.UserName.ToLower()} ou senha inválido.");
                    throw new BadRequestException("Usuário ou senha inválido.");
                }

                _logger.LogInformation($"Usuário {request.UserName.ToLower()} autenticado com sucesso.");


                var userPermission = await _userPermissionRepository.VerifyIfUserExist(userLogin.Id.Value);

                if (userPermission == null)
                {
                    _logger.LogWarning($"Usuário com Id: {userLogin.Id.Value} não encontrado");
                    throw new NotFoundException("Usuário não encontrado.");
                }

                // Gera o token de autenticação usando o serviço de token
                var token = await _tokenService.GenerateToken(userLogin, userPermission);

                _logger.LogInformation($"Token gerado para o usuário {request.UserName.ToLower()}.");


                return new LoginUserResponse() { Token = token };
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Erro durante a execução do manipulador LoginUserHandler: {ex.Message}");
                throw; 
            }
        }
    }
}