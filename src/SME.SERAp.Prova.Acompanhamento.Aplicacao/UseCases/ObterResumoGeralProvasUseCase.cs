using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterResumoGeralProvasUseCase : AbstractUseCase, IObterResumoGeralProvasUseCase
    {
        public ObterResumoGeralProvasUseCase(IMediator mediator) : base(mediator) { }

        public async Task<ResumoGeralDto> Executar(FiltroDto filtro)
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var turmasId = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());

            return await mediator.Send(new ObterResumoGeralProvaQuery(filtro, dresId, uesId, turmasId, filtro.NumeroPagina, filtro.NumeroRegistros));
        }
    }
}
