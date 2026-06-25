using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Interfaces;

namespace MeatFlow.Service.Services;

/// <summary>
/// Implementação do serviço de negócio para <see cref="Estado"/>.
/// </summary>
public class EstadoService : IEstadoService
{
    private readonly IEstadoRepository _estadoRepository;

    /// <summary>Inicializa o serviço com as dependências necessárias.</summary>
    public EstadoService(IEstadoRepository estadoRepository)
    {
        _estadoRepository = estadoRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Estado>> ObterTodosAsync()
        => await _estadoRepository.ObterTodosAsync();
}
