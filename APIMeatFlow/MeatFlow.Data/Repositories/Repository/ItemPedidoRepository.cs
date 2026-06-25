using MeatFlow.Data.Context;
using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeatFlow.Data.Repositories.Repository;

/// <summary>
/// Implementação do repositório de <see cref="ItemPedido"/>.
/// </summary>
public class ItemPedidoRepository : IItemPedidoRepository
{
    private readonly MeatFlowDbContext _context;

    /// <summary>Inicializa o repositório com o contexto do banco de dados.</summary>
    public ItemPedidoRepository(MeatFlowDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ItemPedido>> ObterTodosAsync()
        => await _context.ItensPedido
            .AsNoTracking()
            .Include(x => x.Carne)
            .Include(x => x.Pedido)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<ItemPedido?> ObterPorIdAsync(Guid id)
        => await _context.ItensPedido
            .AsNoTracking()
            .Include(x => x.Carne)
            .Include(x => x.Pedido)
            .FirstOrDefaultAsync(x => x.IdtItemPedido == id);

    /// <inheritdoc />
    public async Task<IEnumerable<ItemPedido>> ObterPorPedidoAsync(Guid idtPedido)
        => await _context.ItensPedido
            .AsNoTracking()
            .Where(x => x.IdtPedido == idtPedido)
            .Include(x => x.Carne)
            .ToListAsync();

    /// <inheritdoc />
    public async Task AdicionarAsync(ItemPedido entidade)
    {
        await _context.ItensPedido.AddAsync(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task AtualizarAsync(ItemPedido entidade)
    {
        _context.ItensPedido.Update(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task RemoverAsync(ItemPedido entidade)
    {
        _context.ItensPedido.Remove(entidade);
        await _context.SaveChangesAsync();
    }
}
