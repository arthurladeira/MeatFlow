namespace MeatFlow.Controller.DTOs.Carne;

/// <summary>
/// Dados necessários para atualizar um registro de carne existente.
/// </summary>
public class UpdateCarneRequest
{
    /// <summary>Descrição da carne.</summary>
    public string DescricaoCarne { get; set; } = string.Empty;

    /// <summary>Origem da carne (ex: bovina, suína, ovina).</summary>
    public string OrigemCarne { get; set; } = string.Empty;
}
