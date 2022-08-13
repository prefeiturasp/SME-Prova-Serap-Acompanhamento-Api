using SME.SERAp.Prova.Acompanhamento.Infra;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IObterAlunosProvaTurmaUseCase
    {
        Task<IEnumerable<AlunoTurmaDto>> Executar(long provaId, long turmaId);
    }
}
