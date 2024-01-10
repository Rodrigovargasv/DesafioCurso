using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            // Configura nome da tabela no banco de dados
            builder.ToTable("permissoes");

            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.Role)
                .HasColumnName("perfil_de_acesso").HasMaxLength(15).IsRequired();

            builder.Property(x => x.UserId).HasColumnName("id_usuario");

            // Relacionamento 1 para 1 entre permissões e usuário
            builder.HasOne(x => x.User)
                .WithOne(x => x.Permission)
                  .HasForeignKey<UserPermission>(p => p.UserId);

            // Identificador unico
            builder.Property(x => x.Identifier)
                 .HasColumnName("Identificador")
                 .IsRequired()
                 .HasMaxLength(10);

            // Configuração de index unico.
            builder.HasIndex(x => x.Identifier).IsUnique();
        }
    }
}