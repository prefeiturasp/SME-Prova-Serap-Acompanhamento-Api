using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterResumoGeralProvaPorUe;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterResumoGeralProvaPorUeUseCase : AbstractUseCase, IObterResumoGeralProvaPorUeUseCase
    {
        public ObterResumoGeralProvaPorUeUseCase(IMediator mediator) : base(mediator) { }

        public async Task<ResumoGeralUnidadePaginadaDto> Executar(FiltroDto filtro, long dreId, long provaId)
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var turmasId = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());
            var ues = await mediator.Send(new ObterUesQuery(dreId, uesId));


            var resumoGeral = new ResumoGeralUnidadePaginadaDto();

            var listaResumoGeralUe = new List<ResumoGeralUnidadeDto>();

            foreach (var ue in ues)
            {

                var retornoResumoGeralProva = await mediator.Send(new ObterResumoGeralProvaPorUeQuery(filtro, long.Parse(ue.Id), provaId, dresId, uesId, turmasId));
                if (retornoResumoGeralProva != null && retornoResumoGeralProva.TotalAlunos > 0)
                {
                    var resumoGeralUe = new ResumoGeralUnidadeDto();
                    resumoGeralUe.Id = long.Parse(ue.Id);
                    resumoGeralUe.Nome = ue.Nome;
                    resumoGeralUe.Turno = string.Empty;
                    retornoResumoGeralProva.CalcularTempoMedio();
                    resumoGeralUe.Item = retornoResumoGeralProva;
                    listaResumoGeralUe.Add(resumoGeralUe);
                }
            }

            resumoGeral.Items = listaResumoGeralUe.OrderBy(x => x.Nome).ToList();
            return resumoGeral;


        }
    }
}
