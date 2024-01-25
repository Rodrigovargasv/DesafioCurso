using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Entities;
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
        public async Task<IEnumerable<GetAllUnitResponse>> GetAllUnit(int page, int pageSize)
        {
            var pagination = new PaginationParamenters() { Page = page, PageSize = pageSize };
            var command = new GetAllUnitRequest() { Paramenters = pagination};

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
            var command = new DeleteUnitRequest() { IdOrIdentifier = idOrIdentifier };

            return await _mediator.Send(command);
            
        }
    }
}