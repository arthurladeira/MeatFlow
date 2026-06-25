namespace MeatFlow.Controller.DTOs.Carne;

/// <summary>
/// Dados necessários para criar um novo registro de carne.
/// </summary>
public class CreateCarneRequest
{
    /// <summary>Descrição da carne.</summary>
    public string DescricaoCarne { get; set; } = string.Empty;

    /// <summary>Origem da carne (ex: bovina, suína, ovina).</summary>
    public string OrigemCarne { get; set; } = string.Empty;
}
