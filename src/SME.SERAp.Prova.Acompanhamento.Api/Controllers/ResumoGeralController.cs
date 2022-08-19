using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Acompanhamento.Aplicacao;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/resumo-geral")]
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
    }
}
