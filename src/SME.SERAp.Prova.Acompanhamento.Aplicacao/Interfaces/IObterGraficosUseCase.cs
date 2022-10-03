using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IObterGraficosUseCase
    {
        Task<GraficosDto> Executar(FiltroDto filtro);
    }
}
