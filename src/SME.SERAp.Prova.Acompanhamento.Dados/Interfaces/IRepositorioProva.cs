using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
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
