using DesafioCurso.Application.Commands.Response.Person;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Person
{
    public class GetAllPersonRequest : IRequest<IEnumerable<GetAllPersonResponse>>
    {
        public int Quantity { get; set; }
    }
}