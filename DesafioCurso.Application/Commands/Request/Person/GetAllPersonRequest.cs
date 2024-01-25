using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Person
{
    public class GetAllPersonRequest : IRequest<IEnumerable<GetAllPersonResponse>>
    {
        public PaginationParamenters Paramenters { get; set; }
    }
}