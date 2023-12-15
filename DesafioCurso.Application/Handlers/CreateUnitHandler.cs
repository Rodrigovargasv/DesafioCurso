
using DesafioCurso.Domain.Commands.Request;
using DesafioCurso.Domain.Commands.Response;
using DesafioCurso.Infra.Data.Handlers.Interfaces;

namespace DesafioCurso.Infra.Ioc.Handlers
{
    public class CreateUnitHandler : ICreateUnitHandler
    {
        public Task<CreateUnitResponse> Handler(CreateUnitRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
