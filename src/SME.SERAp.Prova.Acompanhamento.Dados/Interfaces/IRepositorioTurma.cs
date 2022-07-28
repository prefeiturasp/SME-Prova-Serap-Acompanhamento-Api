using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioTurma : IRepositorioBase<Turma>
    {
        Task<IEnumerable<Turma>> ObterPorUeIdAnoAsync(int anoLetivo, long ueId, Modalidade modalidade, string ano);
    }
}
