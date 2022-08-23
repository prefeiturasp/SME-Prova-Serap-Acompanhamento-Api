using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IObterUeUsuarioLogadoUseCase
    {
        Task<IEnumerable<SelecioneDto>> Executar(long dreId, Modalidade? modalidade);
    }
}
