namespace MeatFlow.Model.Entities;

/// <summary>
/// Representa um tipo de carne disponível para comercialização.
/// </summary>
public class Carne
{
    /// <summary>Identificador único da carne.</summary>
    public Guid IdtCarne { get; set; }

    /// <summary>Descrição da carne.</summary>
    public string DescricaoCarne { get; set; } = string.Empty;

    /// <summary>Origem da carne (ex: bovina, suína, ovina).</summary>
    public string OrigemCarne { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }

    /// <summary>Itens de pedido que referenciam esta carne.</summary>
    public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
}
