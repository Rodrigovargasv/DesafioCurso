
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {

            // Configura nome da tabela
            builder.ToTable("pessoa");

            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();


            builder.Property(x => x.NomeCompleto).HasColumnName("nome_completo").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Documento).HasColumnName("documento").HasMaxLength(14);
            builder.Property(x => x.Cidade).HasMaxLength(30).HasColumnName("cidade").IsRequired();
            builder.Property(x => x.Observacao).HasColumnName("observacao").HasMaxLength(250);
            builder.Property(x => x.CodigoAlternativo).HasColumnName("codigo_alternativo").HasMaxLength(50);
            builder.Property(x => x.LiberaVenda).HasColumnName("libera_venda").HasDefaultValue(false).IsRequired();
            builder.Property(x => x.Ativo).HasColumnName("ativo").HasDefaultValue(true).IsRequired();


            // Cofiguranção de index unico.
            builder.HasIndex(x => x.Documento).IsUnique();
            builder.HasIndex(x => x.CodigoAlternativo).IsUnique();



        }
    }
}
