
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            // Configura nome da tabela
            builder.ToTable("pessoa");

            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();


            builder.Property(x => x.FullName).HasColumnName("nome_completo").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Document).HasColumnName("documento").HasMaxLength(14);
            builder.Property(x => x.City).HasMaxLength(30).HasColumnName("cidade").IsRequired();
            builder.Property(x => x.Observation).HasColumnName("observacao").HasMaxLength(250);
            builder.Property(x => x.AlternativeCode).HasColumnName("codigo_alternativo").HasMaxLength(50);
            builder.Property(x => x.ReleaseSale).HasColumnName("libera_venda").HasDefaultValue(false).IsRequired();
            builder.Property(x => x.Active).HasColumnName("ativo").HasDefaultValue(true).IsRequired();


            // Cofiguranção de index unico.
            builder.HasIndex(x => x.Document).IsUnique();
            builder.HasIndex(x => x.AlternativeCode).IsUnique();



        }
    }
}
