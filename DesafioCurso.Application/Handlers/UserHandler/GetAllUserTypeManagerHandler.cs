using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Enums;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class GetAllUserTypeManagerHandler : IRequestHandler<GetAllUserTypeManagerRequest, IEnumerable<GetAllUserTypeManagerResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserTypeManagerHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetAllUserTypeManagerResponse>> Handle(GetAllUserTypeManagerRequest request, CancellationToken cancellationToken)
        {
            // Busca por todos os usuários do tipo gerente no banco de dados, sendo limitado pela quantidade que o usuário informar no request.
            var users = await _userRepository.GetAllUserByType(request.Paramenters.Page, request.Paramenters.PageSize, UserRole.manager);

            var userReponse = users.Adapt<IEnumerable<GetAllUserTypeManagerResponse>>();

            return userReponse;
        }
    }
}