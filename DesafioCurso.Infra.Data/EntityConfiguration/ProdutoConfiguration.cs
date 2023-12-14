
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Configura nome da tabela no banco de dados
            builder.ToTable("produto");

            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.DescricaoCompleta).HasColumnName("descricao_completa").HasMaxLength(150).IsRequired();
            builder.Property(x => x.DescricaoResumida).HasColumnName("descricao_resumida").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Preco).HasColumnName("preco").HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.QuantidadeEstoque).HasColumnName("quantidade_estoque").HasDefaultValue(0).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.CodigoBarras).HasColumnName("codigo_barras").HasMaxLength(13);
            builder.Property(x => x.Ativo).HasColumnName("ativo").HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Vendavel).HasColumnName("vendavel").HasDefaultValue(false).IsRequired();
            builder.Property(x => x.Unidade).HasColumnName("unidade").HasMaxLength(10).IsRequired();
            builder.Property(x => x.IdUnidade).HasColumnName("id_unidade").IsRequired();


            // Cofiguranção de index unico.
            builder.HasIndex(x => x.CodigoBarras).IsUnique();

            // Configuração de chave estrangeira
            builder.HasOne(p => p.UnidadeProduto)
                .WithMany(u => u.ProdutosRelacionados)
                .HasForeignKey(p => p.IdUnidade);

        }
    }
}
