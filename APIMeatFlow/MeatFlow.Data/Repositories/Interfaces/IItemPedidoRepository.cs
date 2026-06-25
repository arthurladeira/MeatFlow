using MeatFlow.Model.Entities;

namespace MeatFlow.Data.Repositories.Interfaces;

/// <summary>
/// Contrato do repositório de <see cref="ItemPedido"/>.
/// </summary>
public interface IItemPedidoRepository : IRepository<ItemPedido>
{
    /// <summary>Retorna todos os itens de um pedido.</summary>
    Task<IEnumerable<ItemPedido>> ObterPorPedidoAsync(Guid idtPedido);
}
