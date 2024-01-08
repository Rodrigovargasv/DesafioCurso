using DesafioCurso.Application.Commands.Request.UserPermission;
using DesafioCurso.Application.Commands.Response.UserPermission;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCurso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserPermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpPut("UpdateUserPermission/{id:Guid}")]
        public async Task<UpdateUserPermissionResponse> UpdateUserPermission([FromBody] UpdateUserPermissionRequest command, Guid id)
        {
            command.UserId = id;
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpGet("GetAllUserTypeSeller")]
        public async Task<IEnumerable<GetAllUserPermissionResponse>> GetAllUserTypeSeller(int value)
        {
            var command = new GetAllUserPermissionRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }
    }
}