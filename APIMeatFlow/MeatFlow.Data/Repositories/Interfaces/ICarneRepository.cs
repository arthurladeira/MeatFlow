using MeatFlow.Model.Entities;

namespace MeatFlow.Data.Repositories.Interfaces;

/// <summary>
/// Contrato do repositório de <see cref="Carne"/>.
/// </summary>
public interface ICarneRepository : IRepository<Carne>
{
    /// <summary>Retorna a quantidade de pedidos distintos que contêm esta carne.</summary>
    Task<int> ContarPedidosAsync(Guid idtCarne);
}
