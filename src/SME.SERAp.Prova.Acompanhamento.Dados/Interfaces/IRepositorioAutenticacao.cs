using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioAutenticacao : IRepositorioBase<Autenticacao>
    {
        Task<Autenticacao> ObterPorCodigoAsync(string codigo);
        Task<bool> DeletarPorCodigoAsync(string codigo);
    }
}
