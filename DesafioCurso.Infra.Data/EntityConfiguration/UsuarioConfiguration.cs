using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.NomeCompleto).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Apelido).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Cpf_Cnpj).HasMaxLength(14).IsRequired();

            // Configura nome da coluna
            builder.Property(x => x.Cpf_Cnpj).HasColumnName("cpf_cnpj");


            
        }


        
    }
}
