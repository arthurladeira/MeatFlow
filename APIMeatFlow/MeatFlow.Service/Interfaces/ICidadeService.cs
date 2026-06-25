using MeatFlow.Model.Entities;

namespace MeatFlow.Service.Interfaces;

/// <summary>
/// Contrato do serviço de negócio para <see cref="Cidade"/>.
/// </summary>
public interface ICidadeService
{
    /// <summary>Retorna todas as cidades cadastradas.</summary>
    Task<IEnumerable<Cidade>> ObterTodosAsync();
}
