using MeatFlow.Model.Entities;

namespace MeatFlow.Service.Interfaces;

/// <summary>
/// Contrato do serviço de negócio para <see cref="Carne"/>.
/// </summary>
public interface ICarneService
{
    /// <summary>Retorna todas as carnes cadastradas.</summary>
    Task<IEnumerable<Carne>> ObterTodosAsync();

    /// <summary>Retorna uma carne pelo identificador, ou <c>null</c> se não encontrada.</summary>
    Task<Carne?> ObterPorIdAsync(Guid idtCarne);

    /// <summary>Cria um novo registro de carne e retorna a entidade persistida.</summary>
    Task<Carne> CriarAsync(Carne carne);

    /// <summary>Atualiza uma carne existente e retorna a entidade atualizada.</summary>
    Task<Carne> AtualizarAsync(Carne carne);

    /// <summary>Remove uma carne pelo identificador.</summary>
    Task RemoverAsync(Guid idtCarne);
}
