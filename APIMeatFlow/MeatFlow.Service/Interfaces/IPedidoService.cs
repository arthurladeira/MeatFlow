using MeatFlow.Model.Entities;

namespace MeatFlow.Service.Interfaces;

/// <summary>
/// Contrato do serviço de negócio para <see cref="Pedido"/>.
/// </summary>
public interface IPedidoService
{
    /// <summary>Retorna todos os pedidos cadastrados.</summary>
    Task<IEnumerable<Pedido>> ObterTodosAsync();

    /// <summary>Retorna um pedido pelo identificador (com itens incluídos), ou <c>null</c> se não encontrado.</summary>
    Task<Pedido?> ObterPorIdAsync(Guid idtPedido);

    /// <summary>Cria um novo pedido com seus itens e retorna a entidade persistida.</summary>
    Task<Pedido> CriarAsync(Pedido pedido);

    /// <summary>Atualiza o cabeçalho de um pedido existente e retorna a entidade atualizada.</summary>
    Task<Pedido> AtualizarAsync(Pedido pedido);

    /// <summary>Remove um pedido e seus itens pelo identificador.</summary>
    Task RemoverAsync(Guid idtPedido);
}
