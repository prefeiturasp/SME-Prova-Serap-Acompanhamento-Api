using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioAno : IRepositorioBase<Ano>
    {
        Task<IEnumerable<Ano>> ObterPorAnoLetivoUeIdAsync(int anoLetivo, long ueId);
    }
}
