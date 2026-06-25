using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Helpers;
using MeatFlow.Service.Interfaces;

namespace MeatFlow.Service.Services;

/// <summary>
/// Implementação do serviço de negócio para <see cref="Carne"/>.
/// </summary>
public class CarneService : ICarneService
{
    private readonly ICarneRepository _carneRepository;

    /// <summary>Inicializa o serviço com as dependências necessárias.</summary>
    public CarneService(ICarneRepository carneRepository)
    {
        _carneRepository = carneRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Carne>> ObterTodosAsync()
        => await _carneRepository.ObterTodosAsync();

    /// <inheritdoc />
    public async Task<Carne?> ObterPorIdAsync(Guid idtCarne)
        => await _carneRepository.ObterPorIdAsync(idtCarne);

    /// <inheritdoc />
    public async Task<Carne> CriarAsync(Carne carne)
    {
        carne.IdtCarne = Guid.NewGuid();
        carne.DatCriacao = BrasiliaDateTime.Agora;

        await _carneRepository.AdicionarAsync(carne);
        return carne;
    }

    /// <inheritdoc />
    public async Task<Carne> AtualizarAsync(Carne carne)
    {
        carne.DatAtualizacao = BrasiliaDateTime.Agora;

        await _carneRepository.AtualizarAsync(carne);
        return carne;
    }

    /// <inheritdoc />
    public async Task RemoverAsync(Guid idtCarne)
    {
        var carne = await _carneRepository.ObterPorIdAsync(idtCarne)
            ?? throw new KeyNotFoundException($"Carne com ID '{idtCarne}' não encontrada.");

        var totalPedidos = await _carneRepository.ContarPedidosAsync(idtCarne);
        if (totalPedidos > 0)
            throw new InvalidOperationException(
                $"Não é possível excluir a carne \"{carne.DescricaoCarne}\" pois ela está presente em {totalPedidos} pedido(s).");

        await _carneRepository.RemoverAsync(carne);
    }
}
