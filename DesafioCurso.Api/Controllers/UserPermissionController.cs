using DesafioCurso.Application.Commands.Request.UserPermission;
using DesafioCurso.Application.Commands.Response.UserPermission;
using DesafioCurso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        [HttpGet("GetAllPermissionUsers")]
        public async Task<IEnumerable<GetAllUserPermissionResponse>> GetAllPermissionUsers(int page, int pageSize)
        {
            var pagination = new PaginationParamenters() { Page = page, PageSize = pageSize };
            var command = new GetAllUserPermissionRequest() { Paramenters = pagination };
            return await _mediator.Send(command);
        }
    }
}