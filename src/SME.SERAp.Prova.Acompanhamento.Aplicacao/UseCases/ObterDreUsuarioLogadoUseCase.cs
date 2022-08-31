using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterDreUsuarioLogadoUseCase : AbstractUseCase, IObterDreUsuarioLogadoUseCase
    {
        public ObterDreUsuarioLogadoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelecioneDto>> Executar()
        {
            var dresId = await mediator.Send(new ObterDresUsuarioLogadoQuery());
            var dres = await mediator.Send(new ObterDresQuery(dresId));
            if (dres == null && !dres.Any()) return default;

            return dres.Select(s => new SelecioneDto(s.Id, s.Nome)).OrderBy(o => o.Descricao);
        }
    }
}
