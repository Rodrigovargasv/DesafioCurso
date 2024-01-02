using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using MediatR;
using Microsoft.AspNetCore.Http;
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


        [HttpGet("GetAllUnit")]
        public async Task<IEnumerable<GetAllUnitResponse>> GetAllUnit(int value)
        {
            var command = new GetAllUnitRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }



        [HttpGet("GetUnitById/{id:Guid}")]
        public async Task<GetUnitByIdResponse> GetUnitById(Guid id)
        {
            var command = new GetUnitByIdRequest { Id = id };

            return await _mediator.Send(command);
        }


        [HttpPost("CreateUnit")]
        public async Task<CreateUnitResponse> CreateUnit([FromBody] CreateUnitRequest command)
        {
            return await _mediator.Send(command);

        }

        [HttpPut("UpdateUnit/{id:Guid}")]
        public async Task<UpdateUnitResponse> UpdateUnit([FromBody] UpdateUnitRequest command, Guid id)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("DeleteUnit")]
        public async Task<DeleteUnitResponse> DeleteUnit([FromBody] DeleteUnitRequest command)
        {
          
            return await _mediator.Send(command);
        }


    }
}