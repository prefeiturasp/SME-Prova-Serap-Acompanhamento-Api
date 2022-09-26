using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
   public  class GraficosDto
    {
        public List<TotalProvasVsIniciadasDto> TotalProvasVsIniciadas { get; set; }
        public List<TotalProvasVsFinalizadasDto> TotalProvasVsFinalizadas { get; set; }

        public List<QuestoesPrevistasVsQuestoesRespondidasDto> QuestoesPrevistasVsQuestoesRespondidas { get; set;  }

        public List<TempoMedioDto> TempoMedio { get; set; }

    }
}
