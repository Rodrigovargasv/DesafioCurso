using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configura nome da tabela no banco de dados
            builder.ToTable("usuario");

            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.FullName).HasColumnName("nome_completo").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Nickname).HasColumnName("apelido").HasMaxLength(50);
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasColumnName("senha").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Cpf_Cnpj).HasColumnName("cpf_cnpj").HasMaxLength(14);

            // Relacionamento 1 para muitos entre permissões e usuário
            builder.HasOne(u => u.Permission)
            .WithOne(u => u.User)
            .HasForeignKey<UserPermission>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
            // Configuração de index unico.
            builder.HasIndex(x => x.Cpf_Cnpj).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Nickname).IsUnique();
        }
    }
}
