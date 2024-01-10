using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Common.Exceptions;
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

            // Verifica se o usuário existe
            if (userId is null)
                throw new NotFoundException("Usuário não encontrado");

            #region Atualiza as propriedades da usuário com os dados da requisição e faz sua validação de duplicidade de dados

            if (!string.IsNullOrEmpty(request.FullName))
                userId.FullName = request.FullName;

            if (!string.IsNullOrEmpty(request.NickName))
            {
                userId.Nickname = request.NickName;

                // Verifica se o apelido já existe no banco de dados
                var nicknameExist = _userRepository.CheckIfNicknameExist(userId.Nickname);

                if (nicknameExist != null)
                    throw new CustomException("Não possível cadastrar o Apelido, pois ele está indisponível");
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                userId.Email = request.Email;

                // Verifica se o email já existe no banco de dados
                var emailExist = _userRepository.CheckIfdEmailExist(userId.Nickname);

                if (emailExist != null)
                    throw new CustomException("Não possível cadastrar o Email, pois ele está indisponível.");
            }

            if (!string.IsNullOrEmpty(request.Password))
                userId.Password = request.Password;

            if (!string.IsNullOrEmpty(request.Cpf_Cnpj))
            {
                userId.Cpf_Cnpj = request.Cpf_Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                // Verifica se o CPF/CNPJ já existe  no banco de dados
                var Cpf_CnpjExist = _userRepository.CheckIfdEmailExist(userId.Cpf_Cnpj);

                if (Cpf_CnpjExist != null)
                    throw new CustomException("Não possível cadastrar o CPF ou CNPJ, pois eles estão indisponíveis.");
            }

            #endregion Atualiza as propriedades da usuário com os dados da requisição e faz sua validação de duplicidade

            #region Valida dados informados no request e Verifica se CPF/CNPJ já existe no banco de dados na tabela pessoa.
            var userValidation = await _userValidation.ValidateAsync(userId);
           
            if (!userValidation.IsValid) throw new ValidationException(userValidation.Errors);

            var documentInPersonexist = await _personRepository.PropertyDocumentExist(request.Cpf_Cnpj);

            if (documentInPersonexist != null) throw new CustomException("Ja existe um registro de pessoa com esta informação de CPF/CNPJ.");

            #endregion

            _userRepository.Update(userId);
            await _uow.Commit();

            return userId.Adapt<UpdateUserResponse>();
        }
    }
}