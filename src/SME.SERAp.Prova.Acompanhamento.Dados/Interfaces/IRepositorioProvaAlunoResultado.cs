using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados
{
    public interface IRepositorioProvaAlunoResultado : IRepositorioBase<ProvaAlunoResultado>
    {
        Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaTurmaAsync(long provaId, long turmaId);
        Task<IEnumerable<ProvaAlunoResultado>> ObterProvaAlunoResultadoPorFiltroAsync(FiltroDto filtro);
    }
}
