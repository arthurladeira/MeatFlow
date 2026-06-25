using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Interfaces;

namespace MeatFlow.Service.Services;

/// <summary>
/// Implementação do serviço de negócio para <see cref="Cidade"/>.
/// </summary>
public class CidadeService : ICidadeService
{
    private readonly ICidadeRepository _cidadeRepository;

    /// <summary>Inicializa o serviço com as dependências necessárias.</summary>
    public CidadeService(ICidadeRepository cidadeRepository)
    {
        _cidadeRepository = cidadeRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Cidade>> ObterTodosAsync()
        => await _cidadeRepository.ObterTodosAsync();
}
