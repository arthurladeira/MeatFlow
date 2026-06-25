using MeatFlow.Model.Entities;

namespace MeatFlow.Service.Interfaces;

/// <summary>
/// Contrato do serviço de negócio para <see cref="Comprador"/>.
/// </summary>
public interface ICompradorService
{
    /// <summary>Retorna todos os compradores cadastrados.</summary>
    Task<IEnumerable<Comprador>> ObterTodosAsync();

    /// <summary>Retorna um comprador pelo identificador, ou <c>null</c> se não encontrado.</summary>
    Task<Comprador?> ObterPorIdAsync(Guid idtComprador);

    /// <summary>Cria um novo comprador e retorna a entidade persistida.</summary>
    Task<Comprador> CriarAsync(Comprador comprador);

    /// <summary>Atualiza um comprador existente e retorna a entidade atualizada.</summary>
    Task<Comprador> AtualizarAsync(Comprador comprador);

    /// <summary>Remove um comprador pelo identificador.</summary>
    Task RemoverAsync(Guid idtComprador);
}
