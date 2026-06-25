namespace MeatFlow.Data.Repositories.Interfaces;

/// <summary>
/// Contrato base com operações CRUD assíncronas para repositórios genéricos.
/// </summary>
/// <typeparam name="T">Tipo da entidade gerenciada pelo repositório.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>Retorna todas as entidades.</summary>
    Task<IEnumerable<T>> ObterTodosAsync();

    /// <summary>Retorna uma entidade pelo seu identificador único, ou <c>null</c> se não encontrada.</summary>
    Task<T?> ObterPorIdAsync(Guid id);

    /// <summary>Adiciona uma nova entidade e persiste no banco de dados.</summary>
    Task AdicionarAsync(T entidade);

    /// <summary>Atualiza uma entidade existente e persiste no banco de dados.</summary>
    Task AtualizarAsync(T entidade);

    /// <summary>Remove uma entidade e persiste no banco de dados.</summary>
    Task RemoverAsync(T entidade);
}
