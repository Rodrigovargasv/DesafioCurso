using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCurso.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {

        private readonly IMediator _mediator;

     
        public LoginUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<LoginUserResponse> LoginUser([FromBody] LoginUserRequest command)
        {

            return await _mediator.Send(command);
        }
    }
}
