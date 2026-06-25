using MeatFlow.Controller.DTOs.Comprador;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatFlow.Controller.Controllers;

/// <summary>
/// Endpoints para gerenciamento de compradores.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CompradorController : ControllerBase
{
    private readonly ICompradorService _compradorService;

    /// <summary>Inicializa o controller com as dependências necessárias.</summary>
    public CompradorController(ICompradorService compradorService)
    {
        _compradorService = compradorService;
    }

    /// <summary>Retorna todos os compradores cadastrados.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CompradorResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CompradorResponse>>> ObterTodos()
    {
        var compradores = await _compradorService.ObterTodosAsync();
        return Ok(compradores.Select(MapParaResponse));
    }

    /// <summary>Retorna um comprador pelo identificador.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompradorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompradorResponse>> ObterPorId(Guid id)
    {
        var comprador = await _compradorService.ObterPorIdAsync(id);
        if (comprador is null)
            return NotFound();

        return Ok(MapParaResponse(comprador));
    }

    /// <summary>Cria um novo comprador.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CompradorResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<CompradorResponse>> Criar([FromBody] CreateCompradorRequest request)
    {
        var comprador = new Comprador
        {
            DocumentoFiscal = request.DocumentoFiscal,
            NomeComprador = request.NomeComprador,
            IdtCidade = request.IdtCidade
        };

        var criado = await _compradorService.CriarAsync(comprador);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.IdtComprador }, MapParaResponse(criado));
    }

    /// <summary>Atualiza um comprador existente.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CompradorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompradorResponse>> Atualizar(Guid id, [FromBody] UpdateCompradorRequest request)
    {
        var comprador = await _compradorService.ObterPorIdAsync(id);
        if (comprador is null)
            return NotFound();

        comprador.DocumentoFiscal = request.DocumentoFiscal;
        comprador.NomeComprador = request.NomeComprador;
        comprador.IdtCidade = request.IdtCidade;

        var atualizado = await _compradorService.AtualizarAsync(comprador);
        return Ok(MapParaResponse(atualizado));
    }

    /// <summary>Remove um comprador pelo identificador.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Remover(Guid id)
    {
        var comprador = await _compradorService.ObterPorIdAsync(id);
        if (comprador is null)
            return NotFound();

        try
        {
            await _compradorService.RemoverAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    private static CompradorResponse MapParaResponse(Comprador comprador) => new()
    {
        IdtComprador = comprador.IdtComprador,
        DocumentoFiscal = comprador.DocumentoFiscal,
        NomeComprador = comprador.NomeComprador,
        IdtCidade = comprador.IdtCidade,
        NomeCidade = comprador.Cidade?.NomeCidade ?? string.Empty,
        NomeEstado = comprador.Cidade?.Estado?.NomeEstado ?? string.Empty,
        DatCriacao = comprador.DatCriacao,
        DatAtualizacao = comprador.DatAtualizacao
    };
}
