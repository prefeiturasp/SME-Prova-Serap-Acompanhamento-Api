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
    [Route("/api/v1/resumo/turma")]
    [Authorize("Bearer")]
    public class ResumoGeralTurmaController : Controller
    {
        [HttpGet("{turmaId}/prova/{provaId}/alunos")]
        [ProducesResponseType(typeof(List<AlunoTurmaDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterAlunosProvaTurma(long turmaId, long provaId, [FromServices] IObterAlunosProvaTurmaUseCase obterAlunosProvaTurmaUseCase)
        {
            return Ok(await obterAlunosProvaTurmaUseCase.Executar(provaId, turmaId));
        }
    }
}
