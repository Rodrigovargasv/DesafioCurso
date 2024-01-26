using DesafioCurso.Application.Commands.Response.Person;
using DesafioCurso.Domain.Entities;
using MediatR;

namespace DesafioCurso.Application.Commands.Request.Person
{
    public class GetAllPersonRequest : PaginationParamenters, IRequest<IEnumerable<GetAllPersonResponse>>
    {
    }
}