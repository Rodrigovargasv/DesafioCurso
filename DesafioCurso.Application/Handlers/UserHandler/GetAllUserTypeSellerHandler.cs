using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Enums;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserHandler
{
    public class GetAllUserTypeSellerHandler : IRequestHandler<GetAllUserTypeSellerRequest, IEnumerable<GetAllUserTypeSellerResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserTypeSellerHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetAllUserTypeSellerResponse>> Handle(GetAllUserTypeSellerRequest request, CancellationToken cancellationToken)
        {
            // Busca por todos os usuários do tipo vendedor no banco de dados, sendo limitado pela quantidade que o usuário informar no request.
            var users = await _userRepository.GetAllUserByType(request.Quantity, UserRole.seller);

            var userReponse = users.Adapt<IEnumerable<GetAllUserTypeSellerResponse>>();

            return userReponse;
        }
    }
}