namespace MeatFlow.Controller.DTOs.Comprador;

/// <summary>
/// Dados necessários para atualizar um comprador existente.
/// </summary>
public class UpdateCompradorRequest
{
    /// <summary>Documento fiscal do comprador (CPF: 11 dígitos / CNPJ: 14 dígitos).</summary>
    public string DocumentoFiscal { get; set; } = string.Empty;

    /// <summary>Nome do comprador.</summary>
    public string NomeComprador { get; set; } = string.Empty;

    /// <summary>Identificador da cidade do comprador.</summary>
    public Guid IdtCidade { get; set; }
}
