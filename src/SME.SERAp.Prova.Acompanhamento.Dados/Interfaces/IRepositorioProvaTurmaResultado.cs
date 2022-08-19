using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaTurmaResultado : IRepositorioBase<ProvaTurmaResultado>
    {
        Task<IEnumerable<ProvaTurmaResultado>> ObterResumoGeralPorFiltroAsync(long provaId, int? dreId, int? ueId, string anoEscolar, long? turmaId);
        Task<double> ObterTotalProvasFinalizadasPorFiltroAsync(FiltroDto filtro);
        Task<double> ObterTotalProvasIniciadasHojePorFiltroAsync(FiltroDto filtro);
        Task<double> ObterTotalProvasNaoFinalizadasPorFiltroAsync(FiltroDto filtro);
        Task<double> ObterTotalProvasPorFiltroAsync(FiltroDto filtro);
    }
}
