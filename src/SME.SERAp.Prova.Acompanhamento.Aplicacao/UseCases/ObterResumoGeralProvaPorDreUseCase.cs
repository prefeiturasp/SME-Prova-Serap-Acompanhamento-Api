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
    public class ObterResumoGeralProvaPorDreUseCase : AbstractUseCase, IObterResumoGeralPorDreUseCase
    {
        public ObterResumoGeralProvaPorDreUseCase(IMediator mediator) : base(mediator) { }

        public async Task<IEnumerable<ResumoGeralDreDto>> Executar(FiltroDto filtro, long provaId)
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var turmasId = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());
            var dres = await mediator.Send(new ObterDresQuery(null));
            var listaResumoGeralDre = new List<ResumoGeralDreDto>();

            foreach (var dre in dres)
            {

                var retornoResumoGeralProva = await mediator.Send(new ObterResumoGeralProvaPorDreQuery(filtro, long.Parse(dre.Id), provaId, dresId, uesId, turmasId, filtro.NumeroPagina, filtro.NumeroRegistros));
                if (retornoResumoGeralProva != null)
                {
                    var resumoGeralDre = new ResumoGeralDreDto();
                    resumoGeralDre.DreId = long.Parse(dre.Id);
                    resumoGeralDre.DreNome = dre.Nome;
                    resumoGeralDre.Item = retornoResumoGeralProva;
                    listaResumoGeralDre.Add(resumoGeralDre);
                }
            }

            return listaResumoGeralDre;
        }
    }
}