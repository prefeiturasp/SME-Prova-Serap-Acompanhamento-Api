using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/totalizadores")]
    [Authorize("Bearer")]
    public class TotalizadoresController : Controller
    {
        [HttpGet("")]
        [ProducesResponseType(typeof(List<TotalDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterTotais([FromQuery] FiltroDto filtro, [FromServices] IObterTotaisProvasUseCase obterTotaisProvasUseCase)
        {
            return Ok(await obterTotaisProvasUseCase.Executar(filtro));
        }
    }
}


