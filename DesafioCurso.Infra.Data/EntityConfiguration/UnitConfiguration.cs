﻿using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            // Configura nome da tabela no banco de dados
            builder.ToTable("unidade");

            // Configura coluna Id
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();

            builder.Property(x => x.Acronym).HasColumnName("sigla").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Decription).HasColumnName("descricao").HasMaxLength(50).IsRequired();

            // Identificador unico
            builder.Property(x => x.Identifier)
             .HasColumnName("identificador")
             .IsRequired()
             .HasMaxLength(10);

            // Configuração de index unico.
            builder.HasIndex(x => x.Acronym).IsUnique();
            builder.HasIndex(x => x.Identifier).IsUnique();

            // Configuração de chave estrangeira
            builder.HasMany(u => u.RelatedProducts)
                .WithOne(p => p.UnitProduct)
                .HasForeignKey(p => p.AcronynmUnit)
                .HasPrincipalKey(u => u.Acronym)

               // Exclusão da Unit é restrita(ou proibida) se houver entidades relacionadas que dependem dela.
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}