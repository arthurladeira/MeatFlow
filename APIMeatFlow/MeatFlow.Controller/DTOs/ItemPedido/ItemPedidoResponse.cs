namespace MeatFlow.Controller.DTOs.ItemPedido;

/// <summary>
/// Dados retornados pela API para um item de pedido.
/// </summary>
public class ItemPedidoResponse
{
    /// <summary>Identificador único do item de pedido.</summary>
    public Guid IdtItemPedido { get; set; }

    /// <summary>Identificador da carne.</summary>
    public Guid IdtCarne { get; set; }

    /// <summary>Descrição da carne.</summary>
    public string DescricaoCarne { get; set; } = string.Empty;

    /// <summary>Quantidade em quilogramas.</summary>
    public decimal QuantidadeKg { get; set; }

    /// <summary>Valor unitário por quilograma.</summary>
    public decimal ValorUnitario { get; set; }

    /// <summary>Código da moeda utilizada.</summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }
}
