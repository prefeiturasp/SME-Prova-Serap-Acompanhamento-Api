using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioUe : IRepositorioBase<Ue>
    {
        Task<IEnumerable<Ue>> ObterPorIdsAsync(long dreId, long[] ids);
    }
}
