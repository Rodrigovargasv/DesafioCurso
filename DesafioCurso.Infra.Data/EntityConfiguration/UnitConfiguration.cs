
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Intrinsics.X86;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {

        public void Configure(EntityTypeBuilder<Unit> builder)
        {

            // Configura nome da tabela no banco de dados
            builder.ToTable("unidade");


            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.Acronym).HasColumnName("sigla").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Decription).HasColumnName("descricao").HasMaxLength(50).IsRequired();

            // Configuração de index unico.
            builder.HasIndex(x => x.Acronym).IsUnique();


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
