using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Acompanhamento.Aplicacao;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/resumo")]
    [Authorize("Bearer")]
    public class ResumoGeralController : Controller
    {
        [HttpGet("provas")]
        [ProducesResponseType(typeof(ResumoGeralDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterResumoGeralProvas([FromQuery] FiltroDto filtro, [FromServices] IObterResumoGeralProvasUseCase obterResumoGeralProvasUseCase)
        {
            return Ok(await obterResumoGeralProvasUseCase.Executar(filtro));
        }


        [HttpGet("dres/prova/{provaId}")]
        [ProducesResponseType(typeof(ResumoGeralDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterResumoGeralDres([FromQuery] FiltroDto filtro, long provaId, [FromServices] IObterResumoGeralPorDreUseCase obterResumoGeralPorDreUseCase)
        {
            return Ok(await obterResumoGeralPorDreUseCase.Executar(filtro, provaId));
        }


        [HttpGet("ues/dre/{dreId}/prova/{provaId}")]
        [ProducesResponseType(typeof(ResumoGeralDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterResumoGeralUes([FromQuery] FiltroDto filtro, long dreId, long provaId, [FromServices] IObterResumoGeralProvaPorUeUseCase obterResumoGeralProvaPorUeUseCase)
        {
            return Ok(await obterResumoGeralProvaPorUeUseCase.Executar(filtro, dreId, provaId));
        }


        [HttpGet("turmas/ue/{ueId}/prova/{provaId}")]
        [ProducesResponseType(typeof(ResumoGeralDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterResumoGeralTurmas([FromQuery] FiltroDto filtro, long ueId, long provaId, [FromServices] IObterResumoGeralProvaPorTurmaUseCase obterResumoGeralProvaPorTurmaUseCase)
        {
            return Ok(await obterResumoGeralProvaPorTurmaUseCase.Executar(filtro, ueId, provaId));
        }
    }
}