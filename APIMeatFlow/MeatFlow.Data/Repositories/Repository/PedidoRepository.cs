using MeatFlow.Data.Context;
using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeatFlow.Data.Repositories.Repository;

/// <summary>
/// Implementação do repositório de <see cref="Pedido"/>.
/// </summary>
public class PedidoRepository : IPedidoRepository
{
    private readonly MeatFlowDbContext _context;

    /// <summary>Inicializa o repositório com o contexto do banco de dados.</summary>
    public PedidoRepository(MeatFlowDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        => await _context.Pedidos
            .AsNoTracking()
            .Include(x => x.Comprador)
            .Include(x => x.ItensPedido)
                .ThenInclude(i => i.Carne)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<Pedido?> ObterPorIdAsync(Guid id)
        => await _context.Pedidos
            .AsNoTracking()
            .Include(x => x.Comprador)
            .Include(x => x.ItensPedido)
                .ThenInclude(i => i.Carne)
            .FirstOrDefaultAsync(x => x.IdtPedido == id);

    /// <inheritdoc />
    public async Task<IEnumerable<Pedido>> ObterPorCompradorAsync(Guid idtComprador)
        => await _context.Pedidos
            .AsNoTracking()
            .Where(x => x.IdtComprador == idtComprador)
            .Include(x => x.ItensPedido)
                .ThenInclude(i => i.Carne)
            .ToListAsync();

    /// <inheritdoc />
    public async Task AdicionarAsync(Pedido entidade)
    {
        await _context.Pedidos.AddAsync(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task AtualizarAsync(Pedido entidade)
    {
        _context.Pedidos.Update(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task RemoverAsync(Pedido entidade)
    {
        _context.Pedidos.Remove(entidade);
        await _context.SaveChangesAsync();
    }
}
