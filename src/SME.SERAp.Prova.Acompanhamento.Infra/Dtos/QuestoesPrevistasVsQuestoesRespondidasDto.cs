using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
   public class QuestoesPrevistasVsQuestoesRespondidasDto
    {
        public string ProvaDescricao { get; set; }
        public long TotalQuestoesPrevistas { get; set; }
        public decimal TotalQuestoesRespondidas { get; set; }
    }
}
