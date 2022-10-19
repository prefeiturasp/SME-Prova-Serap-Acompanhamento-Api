using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/totalizadores")]
    [Authorize("Bearer")]
    public class TotalizadoresController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<TotalDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterTotais([FromQuery] FiltroDto filtro, [FromServices] IObterTotaisProvasUseCase obterTotaisProvasUseCase)
        {
            return Ok(await obterTotaisProvasUseCase.Executar(filtro));
        }

        [HttpGet("graficos")]
        [ProducesResponseType(typeof(GraficosDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterGraficos([FromQuery] FiltroDto filtro, [FromServices] IObterGraficosUseCase obterGraficosUseCase)
        {
            return Ok(await obterGraficosUseCase.Executar(filtro));
        }
    }
}