using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterAlunosProvaTurmaUseCase : AbstractUseCase, IObterAlunosProvaTurmaUseCase
    {
        public ObterAlunosProvaTurmaUseCase(IMediator mediator) : base(mediator) { }

        public async Task<IEnumerable<AlunoTurmaDto>> Executar(long provaId, long turmaId)
        {
            var result = await mediator.Send(new ObterAlunosProvaTurmaQuery(provaId, turmaId));
            if (result == null || !result.Any()) return default;

            return result.OrderBy(t => t.NomeEstudante);
        }
    }
}
