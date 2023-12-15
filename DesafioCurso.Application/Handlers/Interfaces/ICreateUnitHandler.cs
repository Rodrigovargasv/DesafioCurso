using DesafioCurso.Domain.Commands.Request;
using DesafioCurso.Domain.Commands.Response;

namespace DesafioCurso.Infra.Data.Handlers.Interfaces
{
    public interface ICreateUnitHandler
    {
        Task<CreateUnitResponse> Handler(CreateUnitRequest request);
    }
}
