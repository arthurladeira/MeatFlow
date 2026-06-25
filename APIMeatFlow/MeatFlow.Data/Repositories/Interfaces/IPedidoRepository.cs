using MeatFlow.Model.Entities;

namespace MeatFlow.Data.Repositories.Interfaces;

/// <summary>
/// Contrato do repositório de <see cref="Pedido"/>.
/// </summary>
public interface IPedidoRepository : IRepository<Pedido>
{
    /// <summary>Retorna todos os pedidos de um comprador.</summary>
    Task<IEnumerable<Pedido>> ObterPorCompradorAsync(Guid idtComprador);
}
