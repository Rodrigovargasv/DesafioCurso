﻿using DesafioCurso.Domain.Entities;
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

            // Configura coluna Id
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();

            builder.Property(x => x.FullDescription).HasColumnName("descricao_completa").HasMaxLength(150).IsRequired();
            builder.Property(x => x.BriefDescription).HasColumnName("descricao_resumida").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Price).HasColumnName("preco").HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.QuantityStock).HasColumnName("quantidade_estoque").HasDefaultValue(0).HasPrecision(18, 2);
            builder.Property(x => x.BarCode).HasColumnName("codigo_barras").HasMaxLength(13);
            builder.Property(x => x.Active).HasColumnName("ativo").HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Saleable).HasColumnName("vendavel").HasDefaultValue(false).IsRequired();
            builder.Property(x => x.AcronynmUnit).HasColumnName("unidade").HasMaxLength(10).IsRequired();

            // Identificador unico
            builder.Property(x => x.Identifier)
                 .HasColumnName("identificador")
                 .IsRequired()
                 .HasMaxLength(10);

            // Cofiguranção de index unico.
            builder.HasIndex(x => x.BarCode).IsUnique();
            builder.HasIndex(x => x.Identifier).IsUnique();

            // Configuração de chave estrangeira
            builder.HasOne(p => p.UnitProduct)
                .WithMany(u => u.RelatedProducts)
                .HasForeignKey(p => p.AcronynmUnit);  // Utiliza a propriedade SiglaUnidade como chave estrangeira
        }
    }
}