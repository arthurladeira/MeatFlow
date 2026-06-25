using MeatFlow.Data.Context;
using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeatFlow.Data.Repositories.Repository;

/// <summary>
/// Implementação do repositório de <see cref="Comprador"/>.
/// </summary>
public class CompradorRepository : ICompradorRepository
{
    private readonly MeatFlowDbContext _context;

    /// <summary>Inicializa o repositório com o contexto do banco de dados.</summary>
    public CompradorRepository(MeatFlowDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Comprador>> ObterTodosAsync()
        => await _context.Compradores
            .AsNoTracking()
            .Include(x => x.Cidade)
                .ThenInclude(c => c.Estado)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<Comprador?> ObterPorIdAsync(Guid id)
        => await _context.Compradores
            .AsNoTracking()
            .Include(x => x.Cidade)
                .ThenInclude(c => c.Estado)
            .FirstOrDefaultAsync(x => x.IdtComprador == id);

    /// <inheritdoc />
    public async Task<Comprador?> ObterPorDocumentoFiscalAsync(string documentoFiscal)
        => await _context.Compradores
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DocumentoFiscal == documentoFiscal);

    /// <inheritdoc />
    public async Task AdicionarAsync(Comprador entidade)
    {
        await _context.Compradores.AddAsync(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task AtualizarAsync(Comprador entidade)
    {
        // Anula a navigation property para que o EF use o valor do FK (IdtCidade)
        // e não reverta a alteração com base na entidade Cidade carregada via AsNoTracking
        entidade.Cidade = null!;
        _context.Compradores.Update(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task RemoverAsync(Comprador entidade)
    {
        _context.Compradores.Remove(entidade);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<int> ContarPedidosAsync(Guid idtComprador)
        => await _context.Pedidos.CountAsync(p => p.IdtComprador == idtComprador);
}
