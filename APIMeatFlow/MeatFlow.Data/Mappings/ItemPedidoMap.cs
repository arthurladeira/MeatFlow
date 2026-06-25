using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeatFlow.Data.Mappings;

/// <summary>
/// Configuração do mapeamento Fluent API da entidade <see cref="ItemPedido"/>.
/// </summary>
public class ItemPedidoMap : IEntityTypeConfiguration<ItemPedido>
{
    /// <summary>Aplica as configurações de mapeamento da entidade para o banco de dados.</summary>
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("item_pedido");

        builder.HasKey(x => x.IdtItemPedido);

        builder.Property(x => x.IdtItemPedido)
            .HasColumnName("IDT_ITEM_PEDIDO")
            .IsRequired();

        builder.Property(x => x.IdtPedido)
            .HasColumnName("IDT_PEDIDO")
            .IsRequired();

        builder.Property(x => x.IdtCarne)
            .HasColumnName("IDT_CARNE")
            .IsRequired();

        builder.Property(x => x.QuantidadeKg)
            .HasColumnName("quantidade_kg")
            .HasColumnType("decimal(10,3)")
            .IsRequired();

        builder.Property(x => x.ValorUnitario)
            .HasColumnName("valor_unitario")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.CodigoMoeda)
            .HasColumnName("codigo_moeda")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.DatCriacao)
            .HasColumnName("dat_criacao")
            .IsRequired();

        builder.Property(x => x.DatAtualizacao)
            .HasColumnName("dat_atualizacao")
            .IsRequired();

        // Ao excluir um pedido, seus itens são excluídos automaticamente.
        builder.HasOne(x => x.Pedido)
            .WithMany(x => x.ItensPedido)
            .HasForeignKey(x => x.IdtPedido)
            .OnDelete(DeleteBehavior.Cascade);

        // Não permite excluir uma carne que esteja presente em algum pedido.
        builder.HasOne(x => x.Carne)
            .WithMany(x => x.ItensPedido)
            .HasForeignKey(x => x.IdtCarne)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
