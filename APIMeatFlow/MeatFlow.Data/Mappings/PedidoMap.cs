using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeatFlow.Data.Mappings;

/// <summary>
/// Configuração do mapeamento Fluent API da entidade <see cref="Pedido"/>.
/// </summary>
public class PedidoMap : IEntityTypeConfiguration<Pedido>
{
    /// <summary>Aplica as configurações de mapeamento da entidade para o banco de dados.</summary>
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("pedido");

        builder.HasKey(x => x.IdtPedido);

        builder.Property(x => x.IdtPedido)
            .HasColumnName("IDT_PEDIDO")
            .IsRequired();

        builder.Property(x => x.IdtComprador)
            .HasColumnName("IDT_COMPRADOR")
            .IsRequired();

        builder.Property(x => x.DataPedido)
            .HasColumnName("data_pedido")
            .IsRequired();

        builder.Property(x => x.DatCriacao)
            .HasColumnName("dat_criacao")
            .IsRequired();

        builder.Property(x => x.DatAtualizacao)
            .HasColumnName("dat_atualizacao")
            .IsRequired();

        builder.HasOne(x => x.Comprador)
            .WithMany(x => x.Pedidos)
            .HasForeignKey(x => x.IdtComprador)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ItensPedido)
            .WithOne(x => x.Pedido)
            .HasForeignKey(x => x.IdtPedido)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
