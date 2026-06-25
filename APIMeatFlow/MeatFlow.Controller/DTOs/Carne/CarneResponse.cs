namespace MeatFlow.Controller.DTOs.Carne;

/// <summary>
/// Dados retornados pela API para uma carne.
/// </summary>
public class CarneResponse
{
    /// <summary>Identificador único da carne.</summary>
    public Guid IdtCarne { get; set; }

    /// <summary>Descrição da carne.</summary>
    public string DescricaoCarne { get; set; } = string.Empty;

    /// <summary>Origem da carne.</summary>
    public string OrigemCarne { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }
}
