﻿using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Commons;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Person
{
    public class DeletePesonRequest : EntityBase, IRequest<DeletePersonResponse>
    {
    }
}