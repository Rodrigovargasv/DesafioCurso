
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Sigla).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Descricao).HasMaxLength(50).IsRequired();

            // Cofiguranção de index unico.
            builder.HasIndex(x => x.Sigla).IsUnique();
        }
    }
}
