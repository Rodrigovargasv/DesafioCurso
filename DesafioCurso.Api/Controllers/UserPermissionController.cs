using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Request.UserPermission;
using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Application.Commands.Response.UserPermission;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpPut("UpdateUserPermission/{id:Guid}")]
        public async Task<UpdateUserPermissionResponse> UpdateUserPermission([FromBody] UpdateUserPermissionRequest command, Guid id)
        {
            command.UserId = id;
            return await _mediator.Send(command);

        }

    }
}
