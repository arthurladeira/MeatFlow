using MeatFlow.Data.Context;
using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeatFlow.Data.Repositories.Repository;

/// <summary>
/// Implementação do repositório de <see cref="Carne"/>.
/// </summary>
public class CarneRepository : ICarneRepository
{
    private readonly MeatFlowDbContext _context;

    /// <summary>Inicializa o repositório com o contexto do banco de dados.</summary>
    public CarneRepository(MeatFlowDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Carne>> ObterTodosAsync()
        => await _context.Carnes.AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<Carne?> ObterPorIdAsync(Guid id)
        => await _context.Carnes.AsNoTracking().FirstOrDefaultAsync(x => x.IdtCarne == id);

    /// <inheritdoc />
    public async Task AdicionarAsync(Carne entidade)
    {
        await _context.Carnes.AddAsync(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task AtualizarAsync(Carne entidade)
    {
        _context.Carnes.Update(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task RemoverAsync(Carne entidade)
    {
        _context.Carnes.Remove(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<int> ContarPedidosAsync(Guid idtCarne)
        => await _context.ItensPedido
            .Where(i => i.IdtCarne == idtCarne)
            .Select(i => i.IdtPedido)
            .Distinct() // Um pedido pode ter mais de um item da mesma carne; conta cada pedido apenas uma vez.
            .CountAsync();
}
