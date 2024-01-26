using DesafioCurso.Application.Commands.Response.Person;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Person
{
    public class GetPersonByIdRequest : IRequest<GetPersonByIdResponse>
    {
        public string IdOrIdentifier { get; set; }
    }
}