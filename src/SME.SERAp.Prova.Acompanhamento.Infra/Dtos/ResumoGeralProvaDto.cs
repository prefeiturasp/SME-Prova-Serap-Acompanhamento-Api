
using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra
{
    public class ResumoGeralProvaDto
    {
        public ResumoGeralProvaDto()
        {

        }

        public long ProvaId { get; set; }
        public string TituloProva { get; set; }
        public long TotalAlunos { get; set; }
        public long ProvasIniciadas { get; set; }
        public long ProvasNaoFinalizadas { get; set; }
        public long ProvasFinalizadas { get; set; }
        public long TotalTempoMedio { get; set; }
        public long TempoMedio { get; set; }
        public decimal PercentualRealizado
        {
            get
            {
                if (ProvasFinalizadas == 0 || TotalAlunos == 0) return 0;
                var percentual = (ProvasFinalizadas * 100) / TotalAlunos;
                return percentual > 100 ? 100 : Math.Round(Convert.ToDecimal(percentual), 2);
            }
        }
        public DetalheProvaDto DetalheProva { get; set; }
        public long TotalTurmas { get; set; }

        public void CalcularTempoMedio()
        {
            TempoMedio = TotalTempoMedio > 0 ? (int)(TotalTempoMedio / TotalTurmas) : 0;
        }

        public void CalcularTempoMedioTurma()
        {
            TempoMedio = TotalTempoMedio > 0 ? (int)(TotalTempoMedio / TotalAlunos) : 0;
        }
    }
}