
using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
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

        public CreateUserHandler(IUserRepository userRepository, IUnitOfWork<SqliteDbcontext> uow, 
            UserValidation userValidation, IPersonRepository personRepository, IUserPermissionRepository userPermissionRepository)
        {
            _userRepository = userRepository;
            _uow = uow;
            _userValidation = userValidation;
            _personRepository = personRepository;
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<User>();

            // Formarta CPF E CNPJ para apenas número se caso o usuário passar com caracteres especiais
            if(user.Cpf_Cnpj != null)
                user.Cpf_Cnpj = user.Cpf_Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");


            var userValidation = await _userValidation.ValidateAsync(user);
            if (!userValidation.IsValid)
                throw new ValidationException(userValidation.Errors);

            #region verificação duplicidade de dados
            var Cpf_CPJExist = await _userRepository.CheckIfCPF_CNPJExist(request.Cpf_Cnpj);
            var EmailExist = await _userRepository.CheckIfdEmailExist(request.Email);
            var NicknameExist = await _userRepository.CheckIfNicknameExist(request.Nickname);

            if (Cpf_CPJExist != null)
                throw new CustomException("Não possível cadastrar o CPF ou CNPJ, pois eles estão indisponíveis.");

            if (EmailExist != null)
                throw new CustomException("Não possível cadastrar o Email, pois ele está indisponível.");

            if (NicknameExist != null) 
                throw new CustomException("Não possível cadastrar o Apelido, pois ele está indisponível");

            if (request.Password != request.ConfirmPassword)
                throw new BadRequestException("O dados dos campos senha e confirmação de senha devem ser iguais");

            #endregion

            var documentInPersonexist = await _personRepository.PropertyDocumentExist(request.Cpf_Cnpj);

            if(documentInPersonexist != null) throw new CustomException("Ja existe um registro de pessoa com esta informação de CPF/CNPJ.");

            
            
            await _userRepository.Create(user);
            await _userPermissionRepository.Create(new UserPermission() { Role = UserRole.commonUser, UserId = user.Id.Value});
            await _uow.Commit();
           

            return user.Adapt<CreateUserResponse>();

        }
    }
}
