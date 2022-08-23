using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public class DetalheProvaDto
    {
        public DetalheProvaDto()
        {

        }

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public long QtdeQuestoesProva { get; set; }
        public decimal TotalQuestoes { get; set; }
        public decimal Respondidas { get; set; }
        public decimal PercentualRespondido
        {
            get
            {
                if (TotalQuestoes == 0 || Respondidas == 0) return 0;
                var percentual = (Respondidas * 100) / TotalQuestoes;
                percentual = Convert.ToDecimal(percentual);
                return percentual > 100 ? 100 : Math.Round(percentual, 2);
            }
        }
    }
}