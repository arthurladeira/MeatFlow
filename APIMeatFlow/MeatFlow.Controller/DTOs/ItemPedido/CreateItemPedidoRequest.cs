namespace MeatFlow.Controller.DTOs.ItemPedido;

/// <summary>
/// Dados necessários para criar um item dentro de um pedido.
/// </summary>
public class CreateItemPedidoRequest
{
    /// <summary>Identificador da carne referenciada neste item.</summary>
    public Guid IdtCarne { get; set; }

    /// <summary>Quantidade em quilogramas.</summary>
    public decimal QuantidadeKg { get; set; }

    /// <summary>Valor unitário por quilograma.</summary>
    public decimal ValorUnitario { get; set; }

    /// <summary>Código da moeda (ex: BRL, USD). Máximo 3 caracteres.</summary>
    public string CodigoMoeda { get; set; } = string.Empty;
}
