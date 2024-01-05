
using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Enums;
using DesafioCurso.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

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


            var users = await _userRepository.GetAllUserByType(request.Quantity, UserRole.manager);
            

            var userReponse = users.Adapt<IEnumerable<GetAllUserTypeManagerResponse>>();

            return userReponse;
        }
    }
}
