using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class ProvaTurmaResultado : EntidadeBase
    {
        public ProvaTurmaResultado(long provaId, long dreId, long ueId, long turmaId, string ano, Modalidade modalidade, int anoLetivo, DateTime inicio, DateTime fim, string descricao, long totalAlunos, long totalIniciadas, long totalNaoFinalizados, long totalFinalizados, long quantidadeQuestoes, long totalQuestoes, long questoesRespondidas, long tempoMedio)
        {
            ProvaId = provaId;
            DreId = dreId;
            UeId = ueId;
            TurmaId = turmaId;
            Ano = ano;
            Modalidade = modalidade;
            AnoLetivo = anoLetivo;
            Inicio = inicio;
            Fim = fim;
            Descricao = descricao;
            TotalAlunos = totalAlunos;
            TotalIniciadas = totalIniciadas;
            TotalNaoFinalizados = totalNaoFinalizados;
            TotalFinalizados = totalFinalizados;
            QuantidadeQuestoes = quantidadeQuestoes;
            TotalQuestoes = totalQuestoes;
            QuestoesRespondidas = questoesRespondidas;
            TempoMedio = tempoMedio;
        }

        public long ProvaId { get; set; }
        public long DreId { get; set; }
        public long UeId { get; set; }
        public long TurmaId { get; set; }
        public string Ano { get; set; }
        public Modalidade Modalidade { get; set; }
        public int AnoLetivo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Descricao { get; set; }

        public long TotalAlunos { get; set; }
        public long TotalIniciadas { get; set; }
        public long TotalNaoFinalizados { get; set; }
        public long TotalFinalizados { get; set; }

        public long QuantidadeQuestoes { get; set; }
        public long TotalQuestoes { get; set; }
        public long QuestoesRespondidas { get; set; }
        public long TempoMedio { get; set; }
    }
}
