using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioAbrangencia : IRepositorioBase<Abrangencia>
    {
        Task<IEnumerable<Abrangencia>> ObterPorLoginGrupoAsync(string login, string grupo);
        Task<Abrangencia> ObterPorUsuarioCoressoAsync(string UsuarioCoressoId);
    }
}
