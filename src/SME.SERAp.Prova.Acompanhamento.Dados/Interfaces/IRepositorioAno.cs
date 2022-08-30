using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioAno : IRepositorioBase<Ano>
    {
        Task<IEnumerable<Ano>> ObterPorAnoLetivoModalidadeUeIdAsync(int anoLetivo, Modalidade modalidade, long ueId);
        Task<string[]> ObterUesIdPorAnoLetivoModalidadeAsync(int anoLetivo, Modalidade modalidade, string[] uesId);
    }
}
