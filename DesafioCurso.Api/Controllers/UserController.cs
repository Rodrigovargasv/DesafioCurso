using DesafioCurso.Application.Commands.Request.User;
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
            var command = new GetAllUserRequest() { Quantity = value };
   
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeSeller")]
        public async Task<IEnumerable<GetAllUserTypeSellerResponse>> GetAllUserTypeSeller(int value)
        {
            var command = new GetAllUserTypeSellerRequest() { Quantity = value };

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeManager")]
        public async Task<IEnumerable<GetAllUserTypeManagerResponse>> GetAllUserTypeManager(int value)
        {
            var command = new GetAllUserTypeManagerRequest() { Quantity = value};

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpGet("GetAllUserTypeAdministrator")]
        public async Task<IEnumerable<GetAllUserTypeAdministradorResponse>> GetAllUserTypeAdministrator(int value)
        {
            var command = new GetAllUserTypeAdministratorRequest() { Quantity = value };
    
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