

using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork<SqliteDbcontext> _uow;
        private readonly UserValidation _userValidation;
        private readonly IPersonRepository _personRepository;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork<SqliteDbcontext> uow, UserValidation userValidation, IPersonRepository personRepository)
        {
            _userRepository = userRepository;
            _uow = uow;
            _userValidation = userValidation;
            _personRepository = personRepository;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetById(request.Id);

            // Verifica se a unidade existe
            if (userId is null)
                throw new NotFoundException("Usuário não encontrado");


            #region Atualiza as propriedades da usuário com os dados da requisição

            if (!string.IsNullOrEmpty(request.FullName))
                userId.FullName = request.FullName;

            if (!string.IsNullOrEmpty(request.Surname))
                userId.Nickname = request.Surname;

            if (!string.IsNullOrEmpty(request.Email))
                userId.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Password))
                userId.Password = request.Password;

            if (!string.IsNullOrEmpty(request.Cpf_Cnpj))
                userId.Cpf_Cnpj = request.Cpf_Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            #endregion

            var userValidation = await _userValidation.ValidateAsync(userId);

            if (!userValidation.IsValid) throw new ValidationException(userValidation.Errors);

            var userCheck = await _userRepository.ChecksIfCPF_CNPJAndEmailAndSurnameExists(request.Cpf_Cnpj, request.Email, request.Surname);

            var documentInPersonexist = await _personRepository.PropertyDocumentAndAlternativeCodeExist(request.Cpf_Cnpj, "");

            if (documentInPersonexist != null) throw new CustomException("Ja existe um registro de pessoa com esta informação de CPF/CNPJ.");

            if (userCheck != null) throw new CustomException("Ja existe um usuário com esta informação de CPF/CNPJ, Email ou Apelido.");


            _userRepository.Update(userId);
            await _uow.Commit();

            return userId.Adapt<UpdateUserResponse>();

        }
    }
}
