using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaTurmaResultado : IRepositorioBase<ProvaTurmaResultado>
    {
        Task<ResumoGeralProvaDto> ObterResumoGeralPorFiltroAsync(FiltroDto filtro, long provaId, long[] dresId, long[] uesId);
        Task<double> ObterTotalProvasFinalizadasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId);
        Task<double> ObterTotalProvasIniciadasHojePorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId);
        Task<double> ObterTotalProvasNaoFinalizadasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId);
        Task<double> ObterTotalProvasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId);
    }
}
