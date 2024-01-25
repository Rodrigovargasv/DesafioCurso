using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Entities;
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
        public async Task<IEnumerable<GetAllUserResponse>> GetAllPerson(int page, int pageSize)
        {
            var pagination = new PaginationParamenters() { Page = page, PageSize = pageSize };

            var command = new GetAllUserRequest() {  Paramenters = pagination };
   
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeSeller")]
        public async Task<IEnumerable<GetAllUserTypeSellerResponse>> GetAllUserTypeSeller(int page, int pageSize)
        {
            var pagination = new PaginationParamenters() { Page = page, PageSize = pageSize };

            var command = new GetAllUserTypeSellerRequest() { Paramenters = pagination };

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeManager")]
        public async Task<IEnumerable<GetAllUserTypeManagerResponse>> GetAllUserTypeManager(int page, int pageSize)
        {
            var pagination = new PaginationParamenters() { Page = page, PageSize = pageSize };

            var command = new GetAllUserTypeManagerRequest() { Paramenters = pagination};

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeAdministrator")]
        public async Task<IEnumerable<GetAllUserTypeAdministradorResponse>> GetAllUserTypeAdministrator(int page, int pageSize)
        {
            var pagination = new PaginationParamenters() { Page = page, PageSize = pageSize };

            var command = new GetAllUserTypeAdministratorRequest() { Paramenters = pagination };
    
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetUserById/{idOrIdentifier}")]
        public async Task<GetUserByIdResponse> GetUserById(string idOrIdentifier)
        {
            var command = new GetUserByIdRequest() { IdOrIdentifier = idOrIdentifier };

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator")]
        [HttpPost("CreateUser")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator")]
        [HttpPut("UpdateUser/{idOrIdentifier}")]
        public async Task<UpdateUserResponse> UpdateUser([FromBody] UpdateUserRequest command, string idOrIdentifier)
        {
            command.IdOrIdentifier = idOrIdentifier;
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpDelete("DeleteUser/{idOrIdentifier}")]
        public async Task<DeleteUserResponse> DeleteUser(string idOrIdentifier)
        {
            var command = new DeleteUserRequest() { IdOrIdentifier = idOrIdentifier };

            return await _mediator.Send(command);
        }
    }
}