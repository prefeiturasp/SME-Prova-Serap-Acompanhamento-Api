using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IObterDreUsuarioLogadoUseCase
    {
        Task<IEnumerable<SelecioneDto>> Executar();
    }
}
