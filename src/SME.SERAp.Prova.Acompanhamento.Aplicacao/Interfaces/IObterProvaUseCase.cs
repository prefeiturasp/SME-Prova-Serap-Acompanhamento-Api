using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IObterProvaUseCase
    {
        Task<IEnumerable<SelecioneDto>> Executar(int anoLetivo, ProvaSituacao situacao);
    }
}
