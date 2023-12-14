
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        // Configura contexto do banco de dados
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext) { }


        // Configurar o mapeamento das entidades para o banco de dados
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        // Realiza a aplicação automatica das configurações de entidades que são definidas nas pasta EntityConfiguration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}
