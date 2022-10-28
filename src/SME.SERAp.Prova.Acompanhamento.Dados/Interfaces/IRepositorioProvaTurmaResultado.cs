using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaTurmaResultado : IRepositorioBase<ProvaTurmaResultado>
    {
        Task<ResumoGeralProvaDto> ObterResumoGeralPorFiltroAsync(FiltroDto filtro, long provaId, long[] dresId, long[] uesId, long[] turmasId);
        Task<double> ObterTotalProvasFinalizadasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId);
        Task<double> ObterTotalProvasIniciadasHojePorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId);
        Task<double> ObterTotalProvasNaoFinalizadasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId);
        Task<double> ObterTotalProvasPorFiltroAsync(FiltroDto filtro, long[] dresId, long[] uesId, long[] turmasId);
        Task<ResumoGeralProvaDto> ObterResumoGeralPorDreAsync(FiltroDto filtro, long dreId, long provaId, long[] dresAbrangenciaId, long[] uesAbrangenciaId, long[] turmasAbrangenciaId);
        Task<ResumoGeralProvaDto> ObterResumoGeralPorUeAsync(FiltroDto filtro, long ueId, long provaId, long[] dresAbrangenciaId, long[] uesAbrangenciaId, long[] turmasAbrangenciaId);

        Task<ResumoGeralProvaDto> ObterResumoGeralPorTurmaAsync(FiltroDto filtro, long turmaId, long provaId, long[] dresAbrangenciaId, long[] uesAbrangenciaId, long[] turmasAbrangenciaId);

    }
}
