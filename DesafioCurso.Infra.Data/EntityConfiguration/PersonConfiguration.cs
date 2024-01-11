using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Configura nome da tabela
            builder.ToTable("pessoa");

            // Configura coluna Id
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();

            builder.Property(x => x.FullName).HasColumnName("nome_completo").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Document).HasColumnName("documento").HasMaxLength(14);
            builder.Property(x => x.City).HasMaxLength(30).HasColumnName("cidade").IsRequired();
            builder.Property(x => x.Observation).HasColumnName("observacao").HasMaxLength(250);
            builder.Property(x => x.AlternativeCode).HasColumnName("codigo_alternativo").HasMaxLength(50);
            builder.Property(x => x.ReleaseSale).HasColumnName("libera_venda").HasDefaultValue(false);
            builder.Property(x => x.Active).HasColumnName("ativo").HasDefaultValue(true);

            // Identificador unico
            builder.Property(x => x.Identifier)
                .HasColumnName("identificador")
                .IsRequired()
                .HasMaxLength(10);

            // Configuranção de index unico.
            builder.HasIndex(x => x.Document).IsUnique();
            builder.HasIndex(x => x.AlternativeCode).IsUnique();
            builder.HasIndex(x => x.Identifier).IsUnique();
        }
    }
}