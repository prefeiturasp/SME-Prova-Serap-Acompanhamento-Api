using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
   public class ObterResumoGeralPorDreUseCase : AbstractUseCase, IObterResumoGeralPorDreUseCase
    {
        public ObterResumoGeralPorDreUseCase(IMediator mediator) : base(mediator) { }

        public async Task<ResumoGeralDto> Executar(FiltroDto filtro)
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var turmasId = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());

            return await mediator.Send(new ObterResumoGeralProvaQuery(filtro, dresId, uesId, turmasId, filtro.NumeroPagina, filtro.NumeroRegistros));
        }
    }
}