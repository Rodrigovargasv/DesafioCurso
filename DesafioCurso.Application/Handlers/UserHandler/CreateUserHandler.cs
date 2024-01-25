using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Enums;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork<SqliteDbcontext> _uow;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IShortIdGeneratorService _shortIdGeneratorService;
        private readonly IPasswordManger _passwordManger;

        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork<SqliteDbcontext> uow,
             IUserPermissionRepository userPermissionRepository,
            IShortIdGeneratorService shortIdGenerator, IPasswordManger passwordManger)
        {
            _userRepository = userRepository;
            _uow = uow;
            _userPermissionRepository = userPermissionRepository;
            _shortIdGeneratorService = shortIdGenerator;
            _passwordManger = passwordManger;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<User>();

            // Formarta CPF E CNPJ para apenas número se caso o usuário passar com caracteres especiais
            if (user.Cpf_Cnpj != null)
                user.Cpf_Cnpj = user.Cpf_Cnpj.Replace(".", "").Replace("-", "").Replace("/", ""); ;

            user.Password = _passwordManger.HashPassword(request.Password);

            // Gerar identificador unico para cada usuario criado.
            user.Identifier = _shortIdGeneratorService.GenerateShortId();

            await _userRepository.Create(user);

            // Cria permissões básicas para o novo usuário cadastrado.
            await _userPermissionRepository.Create(new UserPermission()
            {
                Role = UserRole.commonUser,
                UserId = user.Id,
                Identifier =
                _shortIdGeneratorService.GenerateShortId()
            });

            await _uow.Commit();

            return user.Adapt<CreateUserResponse>();
        }
    }
}