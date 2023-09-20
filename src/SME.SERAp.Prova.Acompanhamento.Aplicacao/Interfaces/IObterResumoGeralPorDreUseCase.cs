using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IObterResumoGeralPorDreUseCase
    {
        Task<ResumoGeralUnidadePaginadaDto> Executar(FiltroDto filtro, long provaId);
    }
}
