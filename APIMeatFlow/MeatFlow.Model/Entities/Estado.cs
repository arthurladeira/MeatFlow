namespace MeatFlow.Model.Entities;

/// <summary>
/// Representa um estado brasileiro.
/// </summary>
public class Estado
{
    /// <summary>Identificador único do estado.</summary>
    public Guid IdtEstado { get; set; }

    /// <summary>Sigla do estado (ex: SP, RJ).</summary>
    public string SiglaEstado { get; set; } = string.Empty;

    /// <summary>Nome completo do estado.</summary>
    public string NomeEstado { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }

    /// <summary>Cidades pertencentes a este estado.</summary>
    public ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
}
