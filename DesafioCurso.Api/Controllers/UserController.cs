using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Request.User;
using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Application.Commands.Response.User;
using MediatR;
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

   
        [HttpGet("GetAllUser")]
        public async Task<IEnumerable<GetAllUserResponse>> GetAllPerson(int value)
        {
            var command = new GetAllUserRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }

        [HttpPost("CreateUser")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest command)
        {

            return await _mediator.Send(command);
        }
        
    }
}
