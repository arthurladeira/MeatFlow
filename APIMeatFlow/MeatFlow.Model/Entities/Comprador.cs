namespace MeatFlow.Model.Entities;

/// <summary>
/// Representa um comprador cadastrado no sistema.
/// </summary>
public class Comprador
{
    /// <summary>Identificador único do comprador.</summary>
    public Guid IdtComprador { get; set; }

    /// <summary>Documento fiscal do comprador (CPF ou CNPJ).</summary>
    public string DocumentoFiscal { get; set; } = string.Empty;

    /// <summary>Nome do comprador.</summary>
    public string NomeComprador { get; set; } = string.Empty;

    /// <summary>Identificador da cidade do comprador.</summary>
    public Guid IdtCidade { get; set; }

    /// <summary>Data de criação do registro.</summary>
    public DateTime DatCriacao { get; set; }

    /// <summary>Data da última atualização do registro.</summary>
    public DateTime DatAtualizacao { get; set; }

    /// <summary>Cidade do comprador.</summary>
    public Cidade Cidade { get; set; } = null!;

    /// <summary>Pedidos realizados por este comprador.</summary>
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
