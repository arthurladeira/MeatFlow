using MeatFlow.Model.Entities;

namespace MeatFlow.Service.Interfaces;

/// <summary>
/// Contrato do serviço de negócio para <see cref="Estado"/>.
/// </summary>
public interface IEstadoService
{
    /// <summary>Retorna todos os estados cadastrados.</summary>
    Task<IEnumerable<Estado>> ObterTodosAsync();
}
