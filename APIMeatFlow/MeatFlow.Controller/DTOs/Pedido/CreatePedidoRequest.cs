using MeatFlow.Controller.DTOs.ItemPedido;

namespace MeatFlow.Controller.DTOs.Pedido;

/// <summary>
/// Dados necessários para criar um novo pedido com seus itens.
/// </summary>
public class CreatePedidoRequest
{
    /// <summary>Identificador do comprador que está realizando o pedido.</summary>
    public Guid IdtComprador { get; set; }

    /// <summary>Data em que o pedido foi realizado.</summary>
    public DateTime DataPedido { get; set; }

    /// <summary>Lista de itens do pedido. Deve conter ao menos um item.</summary>
    public ICollection<CreateItemPedidoRequest> Itens { get; set; } = new List<CreateItemPedidoRequest>();
}
