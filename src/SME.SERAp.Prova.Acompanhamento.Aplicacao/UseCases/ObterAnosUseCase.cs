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

        public async Task<IEnumerable<SelecioneDto>> Executar(int anoLetivo, Modalidade modalidade, long euId)
        {
            var anos = await mediator.Send(new ObterAnosQuery(anoLetivo, modalidade, euId));
            if (anos == null && !anos.Any()) return default;

            return anos.Select(s => new SelecioneDto(s.Nome, $"{s.Nome}° Ano")).OrderBy(o => o.Descricao);
        }
    }
}
