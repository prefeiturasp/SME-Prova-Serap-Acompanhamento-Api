using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IAutenticacaoRevalidarUseCase
    {
        Task<AutenticacaoRetornoDto> Executar(AutenticacaoRevalidarDto autenticacaoRevalidarDto);
    }
}
