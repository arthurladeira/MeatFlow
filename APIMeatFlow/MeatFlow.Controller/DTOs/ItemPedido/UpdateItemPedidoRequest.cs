namespace MeatFlow.Controller.DTOs.ItemPedido;

/// <summary>
/// Dados necessários para atualizar ou adicionar um item em um pedido existente.
/// </summary>
public class UpdateItemPedidoRequest
{
    /// <summary>Identificador do item existente. Nulo indica que é um novo item.</summary>
    public Guid? IdtItemPedido { get; set; }

    /// <summary>Identificador da carne referenciada neste item.</summary>
    public Guid IdtCarne { get; set; }

    /// <summary>Quantidade em quilogramas.</summary>
    public decimal QuantidadeKg { get; set; }

    /// <summary>Valor unitário por quilograma.</summary>
    public decimal ValorUnitario { get; set; }

    /// <summary>Código da moeda (ex: BRL, USD). Máximo 3 caracteres.</summary>
    public string CodigoMoeda { get; set; } = string.Empty;
}
