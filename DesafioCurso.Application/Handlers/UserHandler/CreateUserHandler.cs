using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Enums;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork<SqliteDbcontext> _uow;
        private readonly UserValidation _userValidation;
        private readonly IPersonRepository _personRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IShortIdGeneratorService _shortIdGeneratorService;
        private readonly IPasswordManger _passwordManger;

        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork<SqliteDbcontext> uow,
            UserValidation userValidation, IPersonRepository personRepository, IUserPermissionRepository userPermissionRepository,
            IShortIdGeneratorService shortIdGenerator, IPasswordManger passwordManger)
        {
            _userRepository = userRepository;
            _uow = uow;
            _userValidation = userValidation;
            _personRepository = personRepository;
            _userPermissionRepository = userPermissionRepository;
            _shortIdGeneratorService = shortIdGenerator;
            _passwordManger = passwordManger;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<User>();

            // Formarta CPF E CNPJ para apenas número se caso o usuário passar com caracteres especiais
            if (user.Cpf_Cnpj != null)
                user.Cpf_Cnpj = user.Cpf_Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            #region Validações dos dados informados request, verificações da existência dos campos CPF/CNPJ, Email, Apelido no banco de dados e validação de confirmação senha.

            var userValidation = await _userValidation.ValidateAsync(user);

            if (!userValidation.IsValid)
                throw new ValidationException(userValidation.Errors);

            var Cpf_CPJExist = await _userRepository.CheckIfCPF_CNPJExist(request.Cpf_Cnpj);

            if (Cpf_CPJExist != null)
                throw new CustomException("Não possível cadastrar o CPF ou CNPJ, pois eles estão indisponíveis.");

            var EmailExist = await _userRepository.CheckIfdEmailExist(request.Email);

            if (EmailExist != null)
                throw new CustomException("Não possível cadastrar o Email, pois ele está indisponível.");

            var NicknameExist = await _userRepository.CheckIfNicknameExist(request.Nickname);

            if (NicknameExist != null)
                throw new CustomException("Não possível cadastrar o Apelido, pois ele está indisponível");

            if (request.Password != request.ConfirmPassword)
                throw new BadRequestException("O dados dos campos senha e confirmação de senha devem ser iguais");

            #endregion Validações dos dados informados request, verificações da existência dos campos CPF/CNPJ, Email, Apelido no banco de dados e validação de confirmação senha.

            // Verifica já existe um pessoa que possui CPF ou CNPJ cadastrado.
            var documentInPersonexist = await _personRepository.PropertyDocumentExist(request.Cpf_Cnpj);

            if (documentInPersonexist != null) throw new CustomException("Ja existe um registro de pessoa com esta informação de CPF/CNPJ.");

            user.Password = _passwordManger.HashPassword(request.Password);

            // Gerar identificador unico para cada usuario criado.
            user.Identifier = _shortIdGeneratorService.GenerateShortId();

            await _userRepository.Create(user);

            // Cria permissões básicas para o novo usuário cadastrado.
            await _userPermissionRepository.Create(new UserPermission()
            {
                Role = UserRole.commonUser,
                UserId = user.Id.Value,
                Identifier =
                _shortIdGeneratorService.GenerateShortId()
            });

            await _uow.Commit();

            return user.Adapt<CreateUserResponse>();
        }
    }
}