using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Repository
{
    public class UserPermissionRepository : RepositoryBase<UserPermission, SqliteDbcontext>, IUserPermissionRepository
    {
        private readonly SqliteDbcontext _dbcontext; 

        public UserPermissionRepository (SqliteDbcontext context) : base(context)
        {
            _dbcontext = context;
        }

        public async Task<UserPermission> VerifyIfUserExist(Guid id)
        {
            if (id == null)

                // Lida com o caso em que id é nulo
                return null;

            return await _dbcontext.Set<UserPermission>().AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);    
        }
    }
}
