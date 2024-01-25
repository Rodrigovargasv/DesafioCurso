﻿using DesafioCurso.Application.Commands.Response.User;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.User
{
    public class GetAllUserRequest : PaginationParamenters,  IRequest<IEnumerable<GetAllUserResponse>>
    {
        public PaginationParamenters Paramenters { get; set; }
    }
}