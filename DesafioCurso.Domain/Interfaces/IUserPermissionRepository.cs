
using DesafioCurso.Domain.Entities;

namespace DesafioCurso.Domain.Interfaces
{
    public interface IUserPermissionRepository : IRepositoryBase<UserPermission>
    {
      Task<UserPermission> VerifyIfUserExist(Guid id);
    }
}
