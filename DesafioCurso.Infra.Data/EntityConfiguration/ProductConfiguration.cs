
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Configura nome da tabela no banco de dados
            builder.ToTable("produto");

            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.FullDescription).HasColumnName("descricao_completa").HasMaxLength(150).IsRequired();
            builder.Property(x => x.BriefDescription).HasColumnName("descricao_resumida").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Price).HasColumnName("preco").HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.QuantityStock).HasColumnName("quantidade_estoque").HasDefaultValue(0).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.BarCode).HasColumnName("codigo_barras").HasMaxLength(13).IsRequired();
            builder.Property(x => x.Active).HasColumnName("ativo").HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Saleable).HasColumnName("vendavel").HasDefaultValue(false).IsRequired();
            builder.Property(x => x.AcronynmUnit).HasColumnName("unidade").IsRequired();


            // Cofiguranção de index unico.
            builder.HasIndex(x => x.BarCode).IsUnique();

            // Configuração de chave estrangeira
            builder.HasOne(p => p.UnitProduct)
                .WithMany(u => u.RelatedProducts)
                .HasForeignKey(p => p.AcronynmUnit);  // Utiliza a propriedade SiglaUnidade como chave estrangeira
                

        }
    }
}
