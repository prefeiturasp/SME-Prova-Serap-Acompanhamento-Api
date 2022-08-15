using SME.SERAp.Prova.Acompanhamento.Infra;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public interface IResumoGeralProvasUse
    {
        Task<IEnumerable<ResumoGeralProvaDto>> Executar();
    }
}
