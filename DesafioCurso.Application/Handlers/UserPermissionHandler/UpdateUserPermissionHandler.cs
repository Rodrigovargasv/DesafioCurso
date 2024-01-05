﻿
using DesafioCurso.Application.Commands.Request.UserPermission;
using DesafioCurso.Application.Commands.Response.UserPermission;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Enums;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using DesafioCurso.Infra.Data.Repository;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UserPermissionHandler
{
    public class UpdateUserPermissionHandler : IRequestHandler<UpdateUserPermissionRequest, UpdateUserPermissionResponse>
    {

        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IUnitOfWork<SqliteDbcontext> _uow;
        private readonly UserPermissionValidation _userPermissionValiton;

        public UpdateUserPermissionHandler(IUserPermissionRepository userPermissionRepository, 
            IUnitOfWork<SqliteDbcontext> unitOfWork, UserPermissionValidation userPermissionValiton)
        {
            _userPermissionRepository = userPermissionRepository;
            _uow = unitOfWork;
            _userPermissionValiton = userPermissionValiton;
        }

        public async Task<UpdateUserPermissionResponse> Handle(UpdateUserPermissionRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userPermissionRepository.VerifyIfUserExist(request.UserId);

            // Verifica se a unidade existe
            if (userId is null)
                throw new NotFoundException("Usuário não encontrado");


            var userValidation = await _userPermissionValiton.ValidateAsync(userId);

            if (!userValidation.IsValid) throw new ValidationException(userValidation.Errors);


            if (!string.IsNullOrEmpty(request.Role.ToString()))
           
                userId.Role = request.Role;
               
            _userPermissionRepository.Update(userId);
            await _uow.Commit();

            return userId.Adapt<UpdateUserPermissionResponse>();

        }
    }
}
