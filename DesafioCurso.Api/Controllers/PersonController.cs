﻿using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCurso.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreatePerson")]
        public async Task<CreatePersonResponse> CreateUnit([FromBody] CreatePersonRequest command)
        {
            return await _mediator.Send(command);

        }

    }
}