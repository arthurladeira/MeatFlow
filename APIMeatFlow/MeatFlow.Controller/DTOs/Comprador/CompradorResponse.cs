namespace MeatFlow.Controller.DTOs.Comprador;

/// <summary>
/// Dados retornados pela API para um comprador.
/// </summary>
public class CompradorResponse
{
    /// <summary>Identificador único do comprador.</summary>
    public Guid IdtComprador { get; set; }

    /// <summary>Documento fiscal do comprador.</summary>
    public string DocumentoFiscal { get; set; } = string.Empty;

    /// <summary>Nome do comprador.</summary>
    public string NomeComprador { get; set; } = string.Empty;

    /// <summary>Identificador da cidade.</summary>
    public Guid IdtCidade { get; set; }

    /// <summary>Nome da cidade.</summary>
    public string NomeCidade { get; set; } = string.Empty;

    /// <summary>Nome do estado.</summary>
    public string NomeEstado { get; set; } = string.Empty;

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }
}
