using MeatFlow.Model.Entities;

namespace MeatFlow.Data.Repositories.Interfaces;

/// <summary>
/// Contrato do repositório de <see cref="Estado"/>.
/// </summary>
public interface IEstadoRepository
{
    /// <summary>Retorna todos os estados cadastrados.</summary>
    Task<IEnumerable<Estado>> ObterTodosAsync();
}
