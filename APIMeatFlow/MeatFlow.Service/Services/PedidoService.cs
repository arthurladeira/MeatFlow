using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Helpers;
using MeatFlow.Service.Interfaces;

namespace MeatFlow.Service.Services;

/// <summary>
/// Implementação do serviço de negócio para <see cref="Pedido"/>.
/// </summary>
public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IItemPedidoRepository _itemPedidoRepository;

    /// <summary>Inicializa o serviço com as dependências necessárias.</summary>
    public PedidoService(IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _itemPedidoRepository = itemPedidoRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        => await _pedidoRepository.ObterTodosAsync();

    /// <inheritdoc />
    public async Task<Pedido?> ObterPorIdAsync(Guid idtPedido)
        => await _pedidoRepository.ObterPorIdAsync(idtPedido);

    /// <inheritdoc />
    public async Task<Pedido> CriarAsync(Pedido pedido)
    {
        var agora = BrasiliaDateTime.Agora;

        pedido.IdtPedido = Guid.NewGuid();
        pedido.DatCriacao = agora;

        // Preenche os dados de cada item antes de persistir junto com o pedido.
        foreach (var item in pedido.ItensPedido)
        {
            item.IdtItemPedido = Guid.NewGuid();
            item.IdtPedido = pedido.IdtPedido;
            item.DatCriacao = agora;
        }

        await _pedidoRepository.AdicionarAsync(pedido);
        return pedido;
    }

    /// <inheritdoc />
    public async Task<Pedido> AtualizarAsync(Pedido pedido)
    {
        var agora = BrasiliaDateTime.Agora;

        pedido.DatAtualizacao = agora;
        await _pedidoRepository.AtualizarAsync(pedido);

        var itensAtuais = await _itemPedidoRepository.ObterPorPedidoAsync(pedido.IdtPedido);
        var idsManutidos = pedido.ItensPedido
            .Where(i => i.IdtItemPedido != Guid.Empty)
            .Select(i => i.IdtItemPedido)
            .ToHashSet();

        // Remove os itens que não estão mais na lista.
        foreach (var item in itensAtuais.Where(i => !idsManutidos.Contains(i.IdtItemPedido)))
            await _itemPedidoRepository.RemoverAsync(item);

        foreach (var item in pedido.ItensPedido)
        {
            if (item.IdtItemPedido != Guid.Empty)
            {
                // Atualiza item existente.
                item.DatAtualizacao = agora;
                await _itemPedidoRepository.AtualizarAsync(item);
            }
            else
            {
                // Insere novo item.
                item.IdtItemPedido = Guid.NewGuid();
                item.IdtPedido = pedido.IdtPedido;
                item.DatCriacao = agora;
                await _itemPedidoRepository.AdicionarAsync(item);
            }
        }

        return pedido;
    }

    /// <inheritdoc />
    public async Task RemoverAsync(Guid idtPedido)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(idtPedido)
            ?? throw new KeyNotFoundException($"Pedido com ID '{idtPedido}' não encontrado.");

        await _pedidoRepository.RemoverAsync(pedido);
    }
}
