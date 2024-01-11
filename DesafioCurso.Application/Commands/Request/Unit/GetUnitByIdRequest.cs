﻿using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Unit
{
    public class GetUnitByIdRequest : IRequest<GetUnitByIdResponse>
    {
        public Guid Id { get; set; }
    }
}