namespace MeatFlow.Model.Entities;

/// <summary>
/// Representa um item de pedido, associando um tipo de carne a uma quantidade e valor.
/// </summary>
public class ItemPedido
{
    /// <summary>Identificador único do item de pedido.</summary>
    public Guid IdtItemPedido { get; set; }

    /// <summary>Identificador do pedido ao qual este item pertence.</summary>
    public Guid IdtPedido { get; set; }

    /// <summary>Identificador da carne referenciada neste item.</summary>
    public Guid IdtCarne { get; set; }

    /// <summary>Quantidade em quilogramas.</summary>
    public decimal QuantidadeKg { get; set; }

    /// <summary>Valor unitário por quilograma.</summary>
    public decimal ValorUnitario { get; set; }

    /// <summary>Código da moeda utilizada (ex: BRL, USD).</summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }

    /// <summary>Pedido ao qual este item pertence.</summary>
    public Pedido Pedido { get; set; } = null!;

    /// <summary>Carne referenciada neste item.</summary>
    public Carne Carne { get; set; } = null!;
}
