using MeatFlow.Controller.DTOs.ItemPedido;

namespace MeatFlow.Controller.DTOs.Pedido;

/// <summary>
/// Dados retornados pela API para um pedido.
/// </summary>
public class PedidoResponse
{
    /// <summary>Identificador único do pedido.</summary>
    public Guid IdtPedido { get; set; }

    /// <summary>Identificador do comprador.</summary>
    public Guid IdtComprador { get; set; }

    /// <summary>Nome do comprador.</summary>
    public string NomeComprador { get; set; } = string.Empty;

    /// <summary>Data em que o pedido foi realizado.</summary>
    public DateTime DataPedido { get; set; }

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }

    /// <summary>Itens que compõem o pedido.</summary>
    public ICollection<ItemPedidoResponse> Itens { get; set; } = new List<ItemPedidoResponse>();
}
