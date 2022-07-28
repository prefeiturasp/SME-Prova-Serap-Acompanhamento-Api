using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/filtro")]
    [Authorize("Bearer")]
    public class FiltroController : ControllerBase
    {
        [HttpGet("ano-letivo")]
        [ProducesResponseType(typeof(List<SelecioneDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterAnoLetivo([FromServices] IObterAnoLetivoUseCase obterAnoLetivoUseCase)
        {
            return Ok(await obterAnoLetivoUseCase.Executar());
        }

        [HttpGet("situacao")]
        [ProducesResponseType(typeof(List<SelecioneDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterSituacao([FromServices] IObterSituacaoProvaUseCase obterDreUsuarioLogadoUseCase)
        {
            return Ok(await obterDreUsuarioLogadoUseCase.Executar());
        }

        [HttpGet("prova/{anoLetivo}/situacao/{situacao}")]
        [ProducesResponseType(typeof(List<SelecioneDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterProva(int anoLetivo, ProvaSituacao situacao, [FromServices] IObterProvaUseCase obterProvaUseCase)
        {
            return Ok(await obterProvaUseCase.Executar(anoLetivo, situacao));
        }

        [HttpGet("modalidade")]
        [ProducesResponseType(typeof(List<SelecioneDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterModalidade([FromServices] IObterModalidadeUseCase obterModalidadeUseCase)
        {
            return Ok(await obterModalidadeUseCase.Executar());
        }

        [HttpGet("dre")]
        [ProducesResponseType(typeof(List<SelecioneDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterDres([FromServices] IObterDreUsuarioLogadoUseCase obterDreUsuarioLogadoUseCase)
        {
            return Ok(await obterDreUsuarioLogadoUseCase.Executar());
        }

        [HttpGet("ue/{dreId}")]
        [ProducesResponseType(typeof(List<SelecioneDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterUes(long dreId, [FromServices] IObterUeUsuarioLogadoUseCase obterUeUsuarioLogadoUseCase)
        {
            return Ok(await obterUeUsuarioLogadoUseCase.Executar(dreId));
        }

        [HttpGet("ano/{anoLetivo}/ue/{ueId}")]
        [ProducesResponseType(typeof(List<SelecioneDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterAnos(int anoLetivo, long ueId, [FromServices] IObterAnosUseCase obterAnoUseCase)
        {
            return Ok(await obterAnoUseCase.Executar(anoLetivo, ueId));
        }

        [HttpGet("turma/{anoLetivo}/ue/{ueId}/modalidade/{modalidade}/ano/{ano}")]
        [ProducesResponseType(typeof(List<SelecioneDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterTurmas(int anoLetivo, long ueId, Modalidade modalidade, string ano, [FromServices] IObterTurmasUseCase obterTurmasUseCase)
        {
            return Ok(await obterTurmasUseCase.Executar(anoLetivo, ueId, modalidade, ano));
        }
    }
}
