using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCurso.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "administrator, commonUser, manager, seller")]
        [HttpGet("GetAllUnit")]
        public async Task<IEnumerable<GetAllUnitResponse>> GetAllUnit(int value)
        {
            var command = new GetAllUnitRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, commonUser, manager, seller")]
        [HttpGet("GetUnitById/{idOrIdentifier}")]
        public async Task<GetUnitByIdResponse> GetUnitById(string idOrIdentifier)
        {
            var command = new GetUnitByIdRequest { IdOrIdentifier = idOrIdentifier };
  
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpPost("CreateUnit")]
        public async Task<CreateUnitResponse> CreateUnit([FromBody] CreateUnitRequest command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpPut("UpdateUnit/{idOrIdentifier}")]
        public async Task<UpdateUnitResponse> UpdateUnit([FromBody] UpdateUnitRequest command, string idOrIdentifier)
        {
            command.IdOrIdentifier = idOrIdentifier;
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpDelete("DeleteUnit/{idOrIdentifier}")]
        public async Task<DeleteUnitResponse> DeleteUnit(string idOrIdentifier)
        {
            var command = new DeleteUnitRequest();
            command.IdOrIdentifier = idOrIdentifier;

            return await _mediator.Send(command);
            
        }
    }
}