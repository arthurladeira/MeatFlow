namespace MeatFlow.Controller.DTOs.Cidade;

/// <summary>
/// Dados retornados pela API para uma cidade.
/// </summary>
public class CidadeResponse
{
    /// <summary>Identificador único da cidade.</summary>
    public Guid IdtCidade { get; set; }

    /// <summary>Identificador do estado.</summary>
    public Guid IdtEstado { get; set; }

    /// <summary>Nome do estado.</summary>
    public string NomeEstado { get; set; } = string.Empty;

    /// <summary>Nome da cidade.</summary>
    public string NomeCidade { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }
}
