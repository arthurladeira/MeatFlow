using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeatFlow.Data.Mappings;

/// <summary>
/// Configuração do mapeamento Fluent API da entidade <see cref="Estado"/>.
/// </summary>
public class EstadoMap : IEntityTypeConfiguration<Estado>
{
    /// <summary>Aplica as configurações de mapeamento da entidade para o banco de dados.</summary>
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("estado");

        builder.HasKey(x => x.IdtEstado);

        builder.Property(x => x.IdtEstado)
            .HasColumnName("IDT_ESTADO")
            .IsRequired();

        builder.Property(x => x.SiglaEstado)
            .HasColumnName("sigla_estado")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.NomeEstado)
            .HasColumnName("nome_estado")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DatCriacao)
            .HasColumnName("dat_criacao")
            .IsRequired();

        builder.Property(x => x.DatAtualizacao)
            .HasColumnName("dat_atualizacao")
            .IsRequired();

        builder.HasIndex(x => x.SiglaEstado)
            .IsUnique();

        builder.HasMany(x => x.Cidades)
            .WithOne(x => x.Estado)
            .HasForeignKey(x => x.IdtEstado)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
