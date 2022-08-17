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
        public int QtdeQuestoesProva { get; set; }
        public decimal TotalQuestoes { get; set; }
        public decimal Respondidas { get; set; }
        public decimal PercentualRespondido { get; set; }

    }
}