using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeatFlow.Data.Mappings;

/// <summary>
/// Configuração do mapeamento Fluent API da entidade <see cref="Carne"/>.
/// </summary>
public class CarneMap : IEntityTypeConfiguration<Carne>
{
    /// <summary>Aplica as configurações de mapeamento da entidade para o banco de dados.</summary>
    public void Configure(EntityTypeBuilder<Carne> builder)
    {
        builder.ToTable("carne");

        builder.HasKey(x => x.IdtCarne);

        builder.Property(x => x.IdtCarne)
            .HasColumnName("IDT_CARNE")
            .IsRequired();

        builder.Property(x => x.DescricaoCarne)
            .HasColumnName("descricao_carne")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.OrigemCarne)
            .HasColumnName("origem_carne")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DatCriacao)
            .HasColumnName("dat_criacao")
            .IsRequired();

        builder.Property(x => x.DatAtualizacao)
            .HasColumnName("dat_atualizacao")
            .IsRequired();

        builder.HasMany(x => x.ItensPedido)
            .WithOne(x => x.Carne)
            .HasForeignKey(x => x.IdtCarne)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
