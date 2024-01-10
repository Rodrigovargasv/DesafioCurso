using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserRequest, IEnumerable<GetAllUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            // Busca por todos os usuários no banco de dados, sendo limitado pela quantidade que o usuário informar no request.
            var users = await _userRepository.GetAll(request.Quantity);

            var userReponse = users.Adapt<IEnumerable<GetAllUserResponse>>();

            return userReponse;
        }
    }
}