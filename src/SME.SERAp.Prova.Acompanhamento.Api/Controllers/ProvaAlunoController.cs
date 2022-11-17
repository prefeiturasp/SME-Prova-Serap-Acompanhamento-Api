using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Api.Controllers
{

    [ApiController]
    [Route("/api/v1/prova-aluno")]
    [Authorize("Bearer")]
    public class ProvaAlunoController : Controller
    {
        [HttpGet("prova/{provaId}/turma/{turmaId}")]
        [ProducesResponseType(typeof(List<AlunoTurmaDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterAlunosProvaTurma(long turmaId, long provaId, [FromServices] IObterAlunosProvaTurmaUseCase obterAlunosProvaTurmaUseCase)
        {
            return Ok(await obterAlunosProvaTurmaUseCase.Executar(provaId, turmaId));
        }

        [HttpPost("reabrir-prova-aluno")]
        [ProducesResponseType(typeof(ResumoGeralDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ReabrirProvaAluno([FromBody] ReabrirProvaAlunoDto reabrirProvaDto, [FromServices] IReabrirProvaAlunoUseCase reabrirProvaAlunoUseCase)
        {
            return Ok(await reabrirProvaAlunoUseCase.Executar(reabrirProvaDto));
        }
    }
}
