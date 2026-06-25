using MeatFlow.Data.Context;
using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeatFlow.Data.Repositories.Repository;

/// <summary>
/// Implementação do repositório de <see cref="Cidade"/>.
/// </summary>
public class CidadeRepository : ICidadeRepository
{
    private readonly MeatFlowDbContext _context;

    /// <summary>Inicializa o repositório com o contexto do banco de dados.</summary>
    public CidadeRepository(MeatFlowDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Cidade>> ObterTodosAsync()
        => await _context.Cidades
            .AsNoTracking()
            .Include(x => x.Estado)
            .ToListAsync();
}
