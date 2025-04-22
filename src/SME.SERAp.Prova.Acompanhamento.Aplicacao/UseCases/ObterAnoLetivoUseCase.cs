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
            var listaRetorno = new List<SelecioneDto>
            {
                new(2025, "2025"),
                new(2024, "2024"),
                new(2023, "2023"),
                new(2022, "2022")
            };

            return await Task.FromResult(listaRetorno);
        }
    }
}
