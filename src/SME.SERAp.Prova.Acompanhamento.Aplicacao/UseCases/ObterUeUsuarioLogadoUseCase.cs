using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterUeUsuarioLogadoUseCase : AbstractUseCase, IObterUeUsuarioLogadoUseCase
    {
        public ObterUeUsuarioLogadoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelecioneDto>> Executar(long dreId, Modalidade? modalidade )
        {
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var ues = await mediator.Send(new ObterUesQuery(dreId, uesId));

            if (ues == null && !ues.Any()) return default;

            if(modalidade.HasValue && modalidade > 0)
            {
                var uesIdFiltro = ues.Select(x => x.Id).ToArray();
                var anosModalidade = await mediator.Send(new ObterAnosPorModalidadeEscolasQuery(modalidade, uesIdFiltro));
                var idsUesPorModalidade = anosModalidade.GroupBy(x => x.UeId).Select(t => t.Key).ToArray();
                 ues = ues.Where(x => idsUesPorModalidade.Any(id => id.ToString() == x.Id)).ToList();
            }


            return ues.Select(s => new SelecioneDto(s.Id, s.Nome)).OrderBy(o => o.Descricao);
        }
    }
}
