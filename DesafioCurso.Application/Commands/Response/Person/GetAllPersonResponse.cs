﻿
namespace DesafioCurso.Application.Commands.Response.Person;

public record GetAllPersonResponse(Guid Id, string Identifier, string FullName, string Document,
    string City, string Obersavation, string AlternativeCode, string ReleaseSale, string Active);

