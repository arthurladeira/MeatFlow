using MeatFlow.Model.Entities;

namespace MeatFlow.Data.Repositories.Interfaces;

/// <summary>
/// Contrato do repositório de <see cref="Comprador"/>.
/// </summary>
public interface ICompradorRepository : IRepository<Comprador>
{
    /// <summary>Retorna um comprador pelo documento fiscal, ou <c>null</c> se não encontrado.</summary>
    Task<Comprador?> ObterPorDocumentoFiscalAsync(string documentoFiscal);

    /// <summary>Retorna a quantidade de pedidos associados ao comprador.</summary>
    Task<int> ContarPedidosAsync(Guid idtComprador);
}
