using MeatFlow.Controller.DTOs.Estado;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatFlow.Controller.Controllers;

/// <summary>
/// Endpoints para consulta de estados.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EstadoController : ControllerBase
{
    private readonly IEstadoService _estadoService;

    /// <summary>Inicializa o controller com as dependências necessárias.</summary>
    public EstadoController(IEstadoService estadoService)
    {
        _estadoService = estadoService;
    }

    /// <summary>Retorna todos os estados cadastrados.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EstadoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EstadoResponse>>> ObterTodos()
    {
        var estados = await _estadoService.ObterTodosAsync();
        return Ok(estados.Select(MapParaResponse));
    }

    private static EstadoResponse MapParaResponse(Estado estado) => new()
    {
        IdtEstado = estado.IdtEstado,
        SiglaEstado = estado.SiglaEstado,
        NomeEstado = estado.NomeEstado,
        DatCriacao = estado.DatCriacao,
        DatAtualizacao = estado.DatAtualizacao
    };
}
