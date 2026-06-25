using MeatFlow.Controller.DTOs.Cidade;
using MeatFlow.Model.Entities;
using MeatFlow.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeatFlow.Controller.Controllers;

/// <summary>
/// Endpoints para consulta de cidades.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CidadeController : ControllerBase
{
    private readonly ICidadeService _cidadeService;

    /// <summary>Inicializa o controller com as dependências necessárias.</summary>
    public CidadeController(ICidadeService cidadeService)
    {
        _cidadeService = cidadeService;
    }

    /// <summary>Retorna todas as cidades cadastradas.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CidadeResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CidadeResponse>>> ObterTodas()
    {
        var cidades = await _cidadeService.ObterTodosAsync();
        return Ok(cidades.Select(MapParaResponse));
    }

    private static CidadeResponse MapParaResponse(Cidade cidade) => new()
    {
        IdtCidade = cidade.IdtCidade,
        IdtEstado = cidade.IdtEstado,
        NomeEstado = cidade.Estado?.NomeEstado ?? string.Empty,
        NomeCidade = cidade.NomeCidade,
        DatCriacao = cidade.DatCriacao,
        DatAtualizacao = cidade.DatAtualizacao
    };
}
