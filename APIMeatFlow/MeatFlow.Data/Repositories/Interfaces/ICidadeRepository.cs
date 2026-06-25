using MeatFlow.Model.Entities;

namespace MeatFlow.Data.Repositories.Interfaces;

/// <summary>
/// Contrato do repositório de <see cref="Cidade"/>.
/// </summary>
public interface ICidadeRepository
{
    /// <summary>Retorna todas as cidades cadastradas.</summary>
    Task<IEnumerable<Cidade>> ObterTodosAsync();
}
