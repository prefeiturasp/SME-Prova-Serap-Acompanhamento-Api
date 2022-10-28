using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterResumoGeralProvaPorUe;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterResumoGeralProvaPorUeUseCase : AbstractUseCase, IObterResumoGeralProvaPorUeUseCase
    {
        public ObterResumoGeralProvaPorUeUseCase(IMediator mediator) : base(mediator) { }

        public async Task<IEnumerable<ResumoGeralUnidadeDto>> Executar(FiltroDto filtro, long dreId, long provaId)
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var turmasId = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());
            var ues = await mediator.Send(new ObterUesQuery(dreId, uesId));
            var listaResumoGeralUe = new List<ResumoGeralUnidadeDto>();

            foreach (var ue in ues)
            {

                var retornoResumoGeralProva = await mediator.Send(new ObterResumoGeralProvaPorUeQuery(filtro, long.Parse(ue.Id), provaId, dresId, uesId, turmasId));
                if (retornoResumoGeralProva != null)
                {
                    var resumoGeralUe = new ResumoGeralUnidadeDto();
                    resumoGeralUe.Id = long.Parse(ue.Id);
                    resumoGeralUe.Nome = ue.Nome;
                    resumoGeralUe.Item = retornoResumoGeralProva;
                    listaResumoGeralUe.Add(resumoGeralUe);
                }
            }

            return listaResumoGeralUe;
        }
    }
}
