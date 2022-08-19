using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterAnoLetivoUseCase : IObterAnoLetivoUseCase
    {
        public async Task<IEnumerable<SelecioneDto>> Executar()
        {
            return await Task.FromResult(new List<SelecioneDto> { new SelecioneDto(2022, "2022") });
        }
    }
}
