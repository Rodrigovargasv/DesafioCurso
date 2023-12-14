
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.NomeCompleto).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Documento).HasMaxLength(14);
            builder.Property(x => x.Cidade).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Observacao).HasMaxLength(250);
            builder.Property(x => x.CodigoAlternativo).HasMaxLength(50);
            builder.Property(x => x.LiberaVenda).HasDefaultValue(false).IsRequired();
            builder.Property(x => x.Ativo).HasDefaultValue(true).IsRequired();


            // Cofiguranção de index unico.
            builder.HasIndex(x => x.Documento).IsUnique();



        }
    }
}
