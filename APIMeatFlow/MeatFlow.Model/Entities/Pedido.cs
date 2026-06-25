namespace MeatFlow.Model.Entities;

/// <summary>
/// Representa um pedido realizado por um comprador.
/// </summary>
public class Pedido
{
    /// <summary>Identificador único do pedido.</summary>
    public Guid IdtPedido { get; set; }

    /// <summary>Identificador do comprador que realizou o pedido.</summary>
    public Guid IdtComprador { get; set; }

    /// <summary>Data em que o pedido foi realizado.</summary>
    public DateTime DataPedido { get; set; }

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }

    /// <summary>Comprador responsável pelo pedido.</summary>
    public Comprador Comprador { get; set; } = null!;

    /// <summary>Itens que compõem este pedido.</summary>
    public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
}
