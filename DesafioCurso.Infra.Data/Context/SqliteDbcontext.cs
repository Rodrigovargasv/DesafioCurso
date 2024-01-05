using DesafioCurso.Domain.Entities;
using DesafioCurso.Infra.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Context
{
    public class SqliteDbcontext : DbContext
    {
        // Configura contexto do banco de dados
        public SqliteDbcontext(DbContextOptions<SqliteDbcontext> dbContext) : base(dbContext) { }

        // Configurar o mapeamento das entidades para o banco de dados

        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> Permissions { get; set; }

        // Realiza a aplicação automatica das configurações de entidades que são definidas nas pasta EntityConfiguration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Gera e aplica as migrações apenas da classe User e UserPermission.
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new UserPermissionConfiguration());
        }
    }
}