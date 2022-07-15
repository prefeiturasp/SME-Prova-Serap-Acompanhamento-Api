using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioBase<TEntidade> where TEntidade : EntidadeBase
    {
        Task<bool> AlterarAsync(TEntidade entidade);
        Task<bool> CriarIndexAsync();
        Task<bool> DeletarAsync(string id);
        Task<bool> InserirAsync(TEntidade entidade);
        Task<TEntidade> ObterPorIdAsync(string id);
        Task<IEnumerable<TEntidade>> ObterTodosAsync();
    }
}
