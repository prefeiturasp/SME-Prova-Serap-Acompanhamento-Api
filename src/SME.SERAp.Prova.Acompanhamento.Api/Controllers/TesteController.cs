using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Teste")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromServices] IObterTodosTesteUseCase obterTodosTesteUseCase)
        {
            return Ok(await obterTodosTesteUseCase.Executar());
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] InserirTesteDto inserirTesteDto, [FromServices] IInserirTesteUseCase inserirTesteUseCase)
        {
            return Ok(await inserirTesteUseCase.Executar(inserirTesteDto));
        }
    }
}
