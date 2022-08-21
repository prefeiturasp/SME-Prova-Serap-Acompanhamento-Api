using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IObterTurmasUseCase
    {
        Task<IEnumerable<SelecioneDto>> Executar(int anoLetivo, long ueId, Modalidade modalidade, string ano);
    }
}
