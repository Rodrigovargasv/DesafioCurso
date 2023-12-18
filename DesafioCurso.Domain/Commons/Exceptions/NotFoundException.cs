using DesafioCurso.Domain.Common.Exceptions;
using System.Net;

namespace DesafioCurso.Domain.Common.Exceptions;


public class NotFoundException : CustomException
{
    public NotFoundException(string message)
        : base(message, null, HttpStatusCode.NotFound)
    {
    }
}
