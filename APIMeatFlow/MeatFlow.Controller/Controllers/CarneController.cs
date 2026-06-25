using MeatFlow.Controller.DTOs.Carne;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatFlow.Controller.Controllers;

/// <summary>
/// Endpoints para gerenciamento de carnes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CarneController : ControllerBase
{
    private readonly ICarneService _carneService;

    /// <summary>Inicializa o controller com as dependências necessárias.</summary>
    public CarneController(ICarneService carneService)
    {
        _carneService = carneService;
    }

    /// <summary>Retorna todas as carnes cadastradas.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CarneResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CarneResponse>>> ObterTodas()
    {
        var carnes = await _carneService.ObterTodosAsync();
        return Ok(carnes.Select(MapParaResponse));
    }

    /// <summary>Retorna uma carne pelo identificador.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CarneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarneResponse>> ObterPorId(Guid id)
    {
        var carne = await _carneService.ObterPorIdAsync(id);
        if (carne is null)
            return NotFound();

        return Ok(MapParaResponse(carne));
    }

    /// <summary>Cria um novo registro de carne.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CarneResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<CarneResponse>> Criar([FromBody] CreateCarneRequest request)
    {
        var carne = new Carne
        {
            DescricaoCarne = request.DescricaoCarne,
            OrigemCarne = request.OrigemCarne
        };

        var criada = await _carneService.CriarAsync(carne);
        return CreatedAtAction(nameof(ObterPorId), new { id = criada.IdtCarne }, MapParaResponse(criada));
    }

    /// <summary>Atualiza um registro de carne existente.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CarneResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarneResponse>> Atualizar(Guid id, [FromBody] UpdateCarneRequest request)
    {
        var carne = await _carneService.ObterPorIdAsync(id);
        if (carne is null)
            return NotFound();

        carne.DescricaoCarne = request.DescricaoCarne;
        carne.OrigemCarne = request.OrigemCarne;

        var atualizada = await _carneService.AtualizarAsync(carne);
        return Ok(MapParaResponse(atualizada));
    }

    /// <summary>Remove uma carne pelo identificador.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Remover(Guid id)
    {
        var carne = await _carneService.ObterPorIdAsync(id);
        if (carne is null)
            return NotFound();

        try
        {
            await _carneService.RemoverAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    private static CarneResponse MapParaResponse(Carne carne) => new()
    {
        IdtCarne = carne.IdtCarne,
        DescricaoCarne = carne.DescricaoCarne,
        OrigemCarne = carne.OrigemCarne,
        DatCriacao = carne.DatCriacao,
        DatAtualizacao = carne.DatAtualizacao
    };
}
