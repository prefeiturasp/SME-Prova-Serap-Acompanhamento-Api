using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public interface IObterResumoGeralProvasUseCase
    {
        Task<ResumoGeralDto> Executar(FiltroDto filtro);
    }
}
