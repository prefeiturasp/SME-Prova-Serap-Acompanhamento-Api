using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProva : IRepositorioBase<Dominio.Entities.Prova>
    {
        Task<IEnumerable<Dominio.Entities.Prova>> ObterProvaPorAnoLetivoSituacaoAsync(int anoLetivo, ProvaSituacao provaSituacao);
        Task<IEnumerable<Dominio.Entities.Prova>> ObterProvaPorAnoEModalidadeAsync(Dominio.Enums.Modalidade modalidade, int anoLetivo);
    }
}
