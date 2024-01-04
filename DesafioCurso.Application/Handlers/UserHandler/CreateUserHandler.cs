
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

            if(user.Cpf_Cnpj != null)
                user.Cpf_Cnpj = user.Cpf_Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");


            var userValidation = await _userValidation.ValidateAsync(user);

            if (!userValidation.IsValid) throw new ValidationException(userValidation.Errors);

            var userCheck = await _userRepository.CheckIfCPF_CNPJAndEmailAndSurnameExists(request.Cpf_Cnpj, request.Email, request.Surname);

            var documentInPersonexist = await _personRepository.PropertyDocumentAndAlternativeCodeExist(request.Cpf_Cnpj, "");

            if(documentInPersonexist != null) throw new CustomException("Ja existe um registro de pessoa com esta informação de CPF/CNPJ.");

            if (userCheck != null ) throw new CustomException("Ja existe um usuário com esta informação de CPF/CNPJ, Email ou Apelido.");

            
            await _userRepository.Create(user);
            await _userPermissionRepository.Create(new UserPermission() { Role = UserRole.commonUser, UserId = user.Id.Value});
            await _uow.Commit();
           

            return user.Adapt<CreateUserResponse>();

        }
    }
}
