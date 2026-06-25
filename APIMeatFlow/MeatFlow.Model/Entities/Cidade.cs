namespace MeatFlow.Model.Entities;

/// <summary>
/// Representa uma cidade vinculada a um estado.
/// </summary>
public class Cidade
{
    /// <summary>Identificador único da cidade.</summary>
    public Guid IdtCidade { get; set; }

    /// <summary>Identificador do estado ao qual a cidade pertence.</summary>
    public Guid IdtEstado { get; set; }

    /// <summary>Nome da cidade.</summary>
    public string NomeCidade { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }

    /// <summary>Estado ao qual esta cidade pertence.</summary>
    public Estado Estado { get; set; } = null!;

    /// <summary>Compradores domiciliados nesta cidade.</summary>
    public ICollection<Comprador> Compradores { get; set; } = new List<Comprador>();
}
