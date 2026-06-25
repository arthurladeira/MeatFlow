using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Helpers;
using MeatFlow.Service.Interfaces;

namespace MeatFlow.Service.Services;

/// <summary>
/// Implementação do serviço de negócio para <see cref="Comprador"/>.
/// </summary>
public class CompradorService : ICompradorService
{
    private readonly ICompradorRepository _compradorRepository;

    /// <summary>Inicializa o serviço com as dependências necessárias.</summary>
    public CompradorService(ICompradorRepository compradorRepository)
    {
        _compradorRepository = compradorRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Comprador>> ObterTodosAsync()
        => await _compradorRepository.ObterTodosAsync();

    /// <inheritdoc />
    public async Task<Comprador?> ObterPorIdAsync(Guid idtComprador)
        => await _compradorRepository.ObterPorIdAsync(idtComprador);

    /// <inheritdoc />
    public async Task<Comprador> CriarAsync(Comprador comprador)
    {
        comprador.IdtComprador = Guid.NewGuid();
        comprador.DatCriacao = BrasiliaDateTime.Agora;

        await _compradorRepository.AdicionarAsync(comprador);
        return comprador;
    }

    /// <inheritdoc />
    public async Task<Comprador> AtualizarAsync(Comprador comprador)
    {
        comprador.DatAtualizacao = BrasiliaDateTime.Agora;

        await _compradorRepository.AtualizarAsync(comprador);
        return comprador;
    }

    /// <inheritdoc />
    public async Task RemoverAsync(Guid idtComprador)
    {
        var comprador = await _compradorRepository.ObterPorIdAsync(idtComprador)
            ?? throw new KeyNotFoundException($"Comprador com ID '{idtComprador}' não encontrado.");

        var totalPedidos = await _compradorRepository.ContarPedidosAsync(idtComprador);
        if (totalPedidos > 0)
            throw new InvalidOperationException(
                $"Não é possível excluir o comprador \"{comprador.NomeComprador}\" pois ele está presente em {totalPedidos} pedido(s).");

        await _compradorRepository.RemoverAsync(comprador);
    }
}
