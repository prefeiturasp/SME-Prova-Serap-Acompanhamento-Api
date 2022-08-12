using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Dominio
{
    public class ProvaAlunoResultado : EntidadeBase
    {
        public ProvaAlunoResultado(long provaId, long dreId, long ueId, long turmaId, string ano, Modalidade modalidade, int anoLetivo, DateTime inicio, DateTime fim, long alunoId, long alunoRa, string alunoNome, string alunoNomeSocial, bool alunoDownload, DateTime? alunoInicio, DateTime? alunoFim, int? alunoTempoMedio, int? alunoQuestaoRespondida)
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
            AlunoId = alunoId;
            AlunoRa = alunoRa;
            AlunoNome = alunoNome;
            AlunoNomeSocial = alunoNomeSocial;
            AlunoDownload = alunoDownload;
            AlunoInicio = alunoInicio;
            AlunoFim = alunoFim;
            AlunoTempoMedio = alunoTempoMedio;
            AlunoQuestaoRespondida = alunoQuestaoRespondida;

            Id = Guid.NewGuid().ToString();
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
        public long AlunoId { get; set; }
        public long AlunoRa { get; set; }
        public string AlunoNome { get; set; }
        public string AlunoNomeSocial { get; set; }
        public bool AlunoDownload { get; set; }
        public DateTime? AlunoInicio { get; set; }
        public DateTime? AlunoFim { get; set; }
        public int? AlunoTempoMedio { get; set; }
        public int? AlunoQuestaoRespondida { get; set; }
    }
}
