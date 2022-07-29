using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Acompanhamento.Api.Attributes;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/autenticacao")]
    [ChaveAutenticacaoApi]
    public class AutenticacaoController : ControllerBase
    {
        [HttpPost]
        
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        [ProducesResponseType(typeof(AutenticacaoValidarDto), 200)]
        public async Task<IActionResult> Autenticar([FromServices] IAutenticacaoUseCase autenticarUseCase,
            [FromBody] AutenticacaoDto autenticacaoDto)
        {
            return Ok(await autenticarUseCase.Executar(autenticacaoDto));
        }

        [HttpPost("validar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        [ProducesResponseType(typeof(AutenticacaoRetornoDto), 200)]
        public async Task<IActionResult> Validar([FromServices] IAutenticacaoValidarUseCase autenticacaoValidarUseCase,
           [FromBody] AutenticacaoValidarDto autenticacaoValidarDto)
        {
            return Ok(await autenticacaoValidarUseCase.Executar(autenticacaoValidarDto));
        }

        [HttpPost("revalidar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        [ProducesResponseType(typeof(AutenticacaoRetornoDto), 200)]
        public async Task<IActionResult> Revalidar([FromServices] IAutenticacaoRevalidarUseCase autenticacaoRevalidarUseCase,
            [FromBody] AutenticacaoRevalidarDto autenticacaoRevalidarDto)
        {
            return Ok(await autenticacaoRevalidarUseCase.Executar(autenticacaoRevalidarDto));
        }
    }
}
