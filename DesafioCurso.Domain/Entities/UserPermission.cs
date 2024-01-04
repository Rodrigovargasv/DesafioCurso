
using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Enums;

namespace DesafioCurso.Domain.Entities
{
    public class UserPermission : EntityBase
    {

        public UserRole Role { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }    
    }
}
