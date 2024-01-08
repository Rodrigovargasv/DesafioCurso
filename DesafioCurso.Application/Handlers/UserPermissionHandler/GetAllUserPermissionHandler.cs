using DesafioCurso.Application.Commands.Request.UserPermission;
using DesafioCurso.Application.Commands.Response.UserPermission;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserPermissionHandler
{
    public class GetAllUserPermissionHandler : IRequestHandler<GetAllUserPermissionRequest, IEnumerable<GetAllUserPermissionResponse>>
    {
        private readonly IUserPermissionRepository _userPermissionRepository;

        public GetAllUserPermissionHandler(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<IEnumerable<GetAllUserPermissionResponse>> Handle(GetAllUserPermissionRequest request, CancellationToken cancellationToken)
        {
            var users = await _userPermissionRepository.GetAll(request.Quantity);

            var userReponse = users.Adapt<IEnumerable<GetAllUserPermissionResponse>>();

            return userReponse;
        }
    }
}