using MeatFlow.Controller.DTOs.ItemPedido;
using MeatFlow.Controller.DTOs.Pedido;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatFlow.Controller.Controllers;

/// <summary>
/// Endpoints para gerenciamento de pedidos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    /// <summary>Inicializa o controller com as dependências necessárias.</summary>
    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    /// <summary>Retorna todos os pedidos cadastrados.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PedidoResponse>>> ObterTodos()
    {
        var pedidos = await _pedidoService.ObterTodosAsync();
        return Ok(pedidos.Select(MapParaResponse));
    }

    /// <summary>Retorna um pedido pelo identificador, incluindo seus itens.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoResponse>> ObterPorId(Guid id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);
        if (pedido is null)
            return NotFound();

        return Ok(MapParaResponse(pedido));
    }

    /// <summary>Cria um novo pedido com seus itens.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<PedidoResponse>> Criar([FromBody] CreatePedidoRequest request)
    {
        var pedido = new Pedido
        {
            IdtComprador = request.IdtComprador,
            DataPedido = request.DataPedido,
            ItensPedido = request.Itens.Select(i => new ItemPedido
            {
                IdtCarne = i.IdtCarne,
                QuantidadeKg = i.QuantidadeKg,
                ValorUnitario = i.ValorUnitario,
                CodigoMoeda = i.CodigoMoeda
            }).ToList()
        };

        var criado = await _pedidoService.CriarAsync(pedido);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.IdtPedido }, MapParaResponse(criado));
    }

    /// <summary>Atualiza o cabeçalho e os itens de um pedido existente.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoResponse>> Atualizar(Guid id, [FromBody] UpdatePedidoRequest request)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);
        if (pedido is null)
            return NotFound();

        pedido.IdtComprador = request.IdtComprador;
        pedido.DataPedido = request.DataPedido;
        pedido.ItensPedido = request.Itens.Select(i => new ItemPedido
        {
            IdtItemPedido = i.IdtItemPedido ?? Guid.Empty,
            IdtCarne = i.IdtCarne,
            QuantidadeKg = i.QuantidadeKg,
            ValorUnitario = i.ValorUnitario,
            CodigoMoeda = i.CodigoMoeda
        }).ToList();

        var atualizado = await _pedidoService.AtualizarAsync(pedido);
        return Ok(MapParaResponse(atualizado));
    }

    /// <summary>Remove um pedido e seus itens pelo identificador.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(Guid id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);
        if (pedido is null)
            return NotFound();

        await _pedidoService.RemoverAsync(id);
        return NoContent();
    }

    private static PedidoResponse MapParaResponse(Pedido pedido) => new()
    {
        IdtPedido = pedido.IdtPedido,
        IdtComprador = pedido.IdtComprador,
        NomeComprador = pedido.Comprador?.NomeComprador ?? string.Empty,
        DataPedido = pedido.DataPedido,
        DatCriacao = pedido.DatCriacao,
        DatAtualizacao = pedido.DatAtualizacao,
        Itens = pedido.ItensPedido.Select(i => new ItemPedidoResponse
        {
            IdtItemPedido = i.IdtItemPedido,
            IdtCarne = i.IdtCarne,
            DescricaoCarne = i.Carne?.DescricaoCarne ?? string.Empty,
            QuantidadeKg = i.QuantidadeKg,
            ValorUnitario = i.ValorUnitario,
            CodigoMoeda = i.CodigoMoeda,
            DatCriacao = i.DatCriacao,
            DatAtualizacao = i.DatAtualizacao
        }).ToList()
    };
}
