using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IObterAnosUseCase
    {
        Task<IEnumerable<SelecioneDto>> Executar(int anoLetivo, long euId);
    }
}
