﻿using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCurso.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUser")]
        public async Task<IEnumerable<GetAllUserResponse>> GetAllPerson(int value)
        {
            var command = new GetAllUserRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeSeller")]
        public async Task<IEnumerable<GetAllUserTypeSellerResponse>> GetAllUserTypeSeller(int value)
        {
            var command = new GetAllUserTypeSellerRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeManager")]
        public async Task<IEnumerable<GetAllUserTypeManagerResponse>> GetAllUserTypeManager(int value)
        {
            var command = new GetAllUserTypeManagerRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeAdministrator")]
        public async Task<IEnumerable<GetAllUserTypeAdministradorResponse>> GetAllUserTypeAdministrator(int value)
        {
            var command = new GetAllUserTypeAdministratorRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetUserById/{id:Guid}")]
        public async Task<GetUserByIdResponse> GetUserById(Guid id)
        {
            var command = new GetUserByIdRequest();
            command.Id = id;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator")]
        [HttpPost("CreateUser")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator")]
        [HttpPut("UpdateUser/{id:Guid}")]
        public async Task<UpdateUserResponse> UpdateUser([FromBody] UpdateUserRequest command, Guid id)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpDelete("DeleteUser/{id:Guid}")]
        public async Task<DeleteUserResponse> DeleteUser(Guid id)
        {
            var command = new DeleteUserRequest();
            command.Id = id;

            return await _mediator.Send(command);
        }
    }
}