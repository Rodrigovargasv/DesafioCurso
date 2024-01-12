using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Commons;
using MediatR;
using System.Text.Json.Serialization;

namespace DesafioCurso.Application.Commands.Request.Person
{
    public class GetPersonByIdRequest :  IRequest<GetPersonByIdResponse>
    {

        public string IdOrIdentifier { get; set; }

    }
}