namespace MeatFlow.Controller.DTOs.Estado;

/// <summary>
/// Dados retornados pela API para um estado.
/// </summary>
public class EstadoResponse
{
    /// <summary>Identificador único do estado.</summary>
    public Guid IdtEstado { get; set; }

    /// <summary>Sigla do estado.</summary>
    public string SiglaEstado { get; set; } = string.Empty;

    /// <summary>Nome completo do estado.</summary>
    public string NomeEstado { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }
}
