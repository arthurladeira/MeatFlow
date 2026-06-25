using MeatFlow.Controller.DTOs.ItemPedido;

namespace MeatFlow.Controller.DTOs.Pedido;

/// <summary>
/// Dados necessários para atualizar um pedido existente, incluindo seus itens.
/// </summary>
public class UpdatePedidoRequest
{
    /// <summary>Identificador do comprador responsável pelo pedido.</summary>
    public Guid IdtComprador { get; set; }

    /// <summary>Data em que o pedido foi realizado.</summary>
    public DateTime DataPedido { get; set; }

    /// <summary>Lista atualizada de itens do pedido.</summary>
    public ICollection<UpdateItemPedidoRequest> Itens { get; set; } = new List<UpdateItemPedidoRequest>();
}
