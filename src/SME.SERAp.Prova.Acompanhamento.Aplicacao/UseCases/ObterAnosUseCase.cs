using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterAnosUseCase : AbstractUseCase, IObterAnosUseCase
    {
        public ObterAnosUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelecioneDto>> Executar(int anoLetivo, Modalidade modalidade, long ueId)
        {
            var anos = await mediator.Send(new ObterAnosQuery(anoLetivo, modalidade, ueId));
            if (anos == null && !anos.Any()) return default;

            var retorno = new List<SelecioneDto>();
            var turmaIds = await mediator.Send(new ObterTurmasUsuarioLogadoQuery());
            foreach (var ano in anos)
            {
                var turmas = await mediator.Send(new ObterTurmasQuery(anoLetivo, ueId, modalidade, ano.Nome, turmaIds));

                if (turmas != null && turmas.Any())
                    retorno.Add(new SelecioneDto(ano.Nome, $"{ano.Nome}° Ano"));
            }

            return retorno.OrderBy(o => o.Descricao);
        }
    }
}
