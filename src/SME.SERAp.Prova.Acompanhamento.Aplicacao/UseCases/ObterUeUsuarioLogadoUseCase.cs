using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
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

        public async Task<IEnumerable<SelecioneDto>> Executar(long dreId)
        {
            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());
            var ues = await mediator.Send(new ObterUesQuery(dreId, uesId));
            if (ues == null && !ues.Any()) return default;

            return ues.Select(s => new SelecioneDto(s.Id, s.Nome)).OrderBy(o => o.Descricao);
        }
    }
}
