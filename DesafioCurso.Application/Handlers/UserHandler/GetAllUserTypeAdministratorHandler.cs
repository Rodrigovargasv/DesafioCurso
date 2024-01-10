using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Enums;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class GetAllUserTypeAdministratorHandler : IRequestHandler<GetAllUserTypeAdministratorRequest, IEnumerable<GetAllUserTypeAdministradorResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserTypeAdministratorHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetAllUserTypeAdministradorResponse>> Handle(GetAllUserTypeAdministratorRequest request, CancellationToken cancellationToken)
        {
            // Busca por todos os usuários do administrador no banco de dados, sendo limitado pela quantidade que o usuário informar no request.
            var users = await _userRepository.GetAllUserByType(request.Quantity, UserRole.administrator);

            var userReponse = users.Adapt<IEnumerable<GetAllUserTypeAdministradorResponse>>();

            return userReponse;
        }
    }
}