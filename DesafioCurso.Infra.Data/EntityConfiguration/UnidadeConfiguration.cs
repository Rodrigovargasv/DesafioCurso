
using DesafioCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioCurso.Infra.Data.EntityConfiguration
{
    public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
    {

        public void Configure(EntityTypeBuilder<Unidade> builder)
        {

            // Configura nome da tabela no banco de dados
            builder.ToTable("unidade");


            // Configura o banco de dados para gerar automaticamente o id
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.Sigla).HasColumnName("sigla").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(50).IsRequired();

            // Configuração de index unico.
            builder.HasIndex(x => x.Sigla).IsUnique();


            // Configuração de chave estrangeira
            builder.HasMany(u => u.ProdutosRelacionados)
                .WithOne(p => p.UnidadeProduto)
                .HasForeignKey(p => p.SiglaUnidade) 
                .HasPrincipalKey(u => u.Sigla);
            

        }
    }
}
