using DesafioCurso.Application.Commands.Response;
using MediatR;


namespace DesafioCurso.Application.Commands.Request
{
    public class CreatePersonRequest : IRequest<CreatePersonResponse>
    {
       

        public string FullName { get; set; }
        public string? Document { get; set; }
        public string City { get; set; }
        public string Observation { get; set; }
        public string? AlternativeCode { get; set; }

    }
}
