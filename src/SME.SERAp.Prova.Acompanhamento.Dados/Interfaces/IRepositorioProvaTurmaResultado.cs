using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaTurmaResultado : IRepositorioBase<ProvaTurmaResultado>
    {
        Task<IEnumerable<ProvaTurmaResultado>> ObterResumoGeralPorFiltroAsync(long provaId, int? dreId, int? ueId, string anoEscolar, long? turmaId, long[] dresId, long[] uesId);
        Task<IEnumerable<ProvaTurmaResultado>> ObterResumoGeralPorFiltroAsync2(FiltroDto filtro, long provaId, long[] dresId, long[] uesId);
        Task<double> ObterTotalProvasFinalizadasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId);
        Task<double> ObterTotalProvasIniciadasHojePorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId);
        Task<double> ObterTotalProvasNaoFinalizadasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId);
        Task<double> ObterTotalProvasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId);
    }
}
