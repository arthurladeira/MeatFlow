using MeatFlow.Data.Context;
using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeatFlow.Data.Repositories.Repository;

/// <summary>
/// Implementação do repositório de <see cref="Estado"/>.
/// </summary>
public class EstadoRepository : IEstadoRepository
{
    private readonly MeatFlowDbContext _context;

    /// <summary>Inicializa o repositório com o contexto do banco de dados.</summary>
    public EstadoRepository(MeatFlowDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Estado>> ObterTodosAsync()
        => await _context.Estados.AsNoTracking().ToListAsync();
}
