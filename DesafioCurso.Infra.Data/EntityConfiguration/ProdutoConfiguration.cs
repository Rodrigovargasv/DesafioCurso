
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.DescricaoCompleta).HasMaxLength(150).IsRequired();
            builder.Property(x => x.DescricaoResumida).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Preco).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.QuantidadeEstoque).HasDefaultValue(0).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.CodigoBarras).HasMaxLength(13);
            builder.Property(x => x.Ativo).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Vendavel).HasDefaultValue(false).IsRequired();
            builder.Property(x => x.Unidade).HasMaxLength(10).IsRequired();


            // Cofiguranção de index unico.
            builder.HasIndex(x => x.CodigoBarras).IsUnique();

            // Configuração de chave estrangeira
            builder.HasOne(p => p.Unidade)
                .WithMany(u => u.Produtos)
                .HasForeignKey(p => p.IdUnidade);

        }
    }
}
