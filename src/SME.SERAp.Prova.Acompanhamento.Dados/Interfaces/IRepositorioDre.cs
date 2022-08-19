using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioDre : IRepositorioBase<Dre>
    {
        Task<IEnumerable<Dre>> obterPorIdsAsync(long[] ids);
        Task<IEnumerable<Dre>> ObterTodasAsync();
    }
}
