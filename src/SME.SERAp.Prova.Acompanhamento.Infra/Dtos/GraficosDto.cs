using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public  class GraficosDto
    {
        public List<GraficoItemDto> TotalProvasVsIniciadas { get; set; }
        public List<GraficoItemDto> TotalProvasVsFinalizadas { get; set; }

        public List<GraficoItemDto> QuestoesPrevistasVsQuestoesRespondidas { get; set;  }

        public List<GraficoItemDto> ProvaVSTempoMedio { get; set; }

    }
}
