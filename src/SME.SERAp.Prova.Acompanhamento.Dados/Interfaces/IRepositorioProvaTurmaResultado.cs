using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaTurmaResultado : IRepositorioBase<ProvaTurmaResultado>
    {
        Task<IEnumerable<ProvaTurmaResultado>> ObterResumoGeralPorFiltroAsync(long provaId, int? dreId, int? ueId, string anoEscolar, long? turmaId);
    }
}
