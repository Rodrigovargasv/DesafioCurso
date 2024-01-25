using DesafioCurso.Domain.Entities;
using DesafioCurso.Infra.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        // Configura contexto do banco de dados
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext) { }

        // Configurar o mapeamento das entidades para o banco de dados
        public DbSet<Person> People { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Units { get; set; }


        // Realiza a aplicação automatica das configurações de entidades que são definidas nas pasta EntityConfiguration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PersonConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new UnitConfiguration());
        }
    }
}