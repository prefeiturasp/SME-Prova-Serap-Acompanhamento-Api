using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class TotalProvasDto
    {
        public List<TotalDto> Totais { get; set; }
        public GraficosDto Graficos { get; set; }
    }
}
