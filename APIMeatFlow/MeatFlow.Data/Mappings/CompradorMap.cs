using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeatFlow.Data.Mappings;

/// <summary>
/// Configuração do mapeamento Fluent API da entidade <see cref="Comprador"/>.
/// </summary>
public class CompradorMap : IEntityTypeConfiguration<Comprador>
{
    /// <summary>Aplica as configurações de mapeamento da entidade para o banco de dados.</summary>
    public void Configure(EntityTypeBuilder<Comprador> builder)
    {
        builder.ToTable("comprador");

        builder.HasKey(x => x.IdtComprador);

        builder.Property(x => x.IdtComprador)
            .HasColumnName("IDT_COMPRADOR")
            .IsRequired();

        builder.Property(x => x.DocumentoFiscal)
            .HasColumnName("documento_fiscal")
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(x => x.NomeComprador)
            .HasColumnName("nome_comprador")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.IdtCidade)
            .HasColumnName("IDT_CIDADE")
            .IsRequired();

        builder.Property(x => x.DatCriacao)
            .HasColumnName("dat_criacao")
            .IsRequired();

        builder.Property(x => x.DatAtualizacao)
            .HasColumnName("dat_atualizacao")
            .IsRequired();

        builder.HasIndex(x => x.DocumentoFiscal)
            .IsUnique();

        builder.HasOne(x => x.Cidade)
            .WithMany(x => x.Compradores)
            .HasForeignKey(x => x.IdtCidade)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Pedidos)
            .WithOne(x => x.Comprador)
            .HasForeignKey(x => x.IdtComprador)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
