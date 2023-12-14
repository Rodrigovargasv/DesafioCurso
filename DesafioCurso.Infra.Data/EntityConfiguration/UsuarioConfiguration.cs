using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Configura nome da tabela no banco de dados
            builder.ToTable("usuario");

            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.NomeCompleto).HasColumnName("nome_completo").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Apelido).HasColumnName("apelido").HasMaxLength(50);
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Senha).HasColumnName("senha").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Cpf_Cnpj).HasColumnName("cpf_cnpj").HasMaxLength(14).IsRequired();

            
        }
    }
}
